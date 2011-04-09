using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Microsoft.Win32.SafeHandles;

internal class Transfer {

    private NameValueCollection Items;

    public Transfer(string command, string serviceName) {
        this.Items = new NameValueCollection();
        this.Items.Add("Command", command);
        this.Items.Add("ServiceName", serviceName);
    }

    private Transfer(NameValueCollection items) {
        this.Items = items;
    }


    public string Command { get { return this.Items["Command"]; } }

    public string ServiceName { get { return this.Items["ServiceName"]; } }



    public override string ToString() {
        return this.Command;
    }



    private static byte[] Serialize(Transfer transfer) {
        using (var ms = new MemoryStream()) {
            var bf = new BinaryFormatter();
            bf.Serialize(ms, transfer.Items);
            ms.Flush();
            return ms.GetBuffer();
        }
    }

    private static Transfer Deserialize(byte[] buffer) {
        using (var ms = new MemoryStream(buffer)) {
            ms.Position = 0;
            var bf = new BinaryFormatter();
            return new Transfer(bf.Deserialize(ms) as NameValueCollection);
        }
    }


    public static void Send(Transfer transfer) {
        NativeMethods.FileSafeHandle handle = null;

        try {
            var buffer = Serialize(transfer);

            //open pipe
            if (NativeMethods.WaitNamedPipe(NamedPipeName, NativeMethods.NMPWAIT_USE_DEFAULT_WAIT) == false) { throw new Win32Exception(); }
            handle = NativeMethods.CreateFile(NamedPipeName, NativeMethods.GENERIC_READ | NativeMethods.GENERIC_WRITE, 0, System.IntPtr.Zero, NativeMethods.OPEN_EXISTING, NativeMethods.FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);
            if (handle.IsInvalid) { throw new Win32Exception(); }

            //send bytes
            uint written = 0;
            NativeOverlapped overlapped = new NativeOverlapped();
            if (NativeMethods.WriteFile(handle, buffer, (uint)buffer.Length, ref written, ref overlapped) == false) { throw new Win32Exception(); }
            if (written != buffer.Length) { throw new InvalidOperationException("Invalid byte count."); }
        } finally {
            if (handle != null) {
                handle.Close();
            }
        }
    }


    public static SafeHandleMinusOneIsInvalid Create() {
        var handle = NativeMethods.CreateNamedPipe(Transfer.NamedPipeName, NativeMethods.PIPE_ACCESS_DUPLEX, NativeMethods.PIPE_TYPE_BYTE | NativeMethods.PIPE_READMODE_BYTE | NativeMethods.PIPE_WAIT, NativeMethods.PIPE_UNLIMITED_INSTANCES, 4096, 4096, NativeMethods.NMPWAIT_USE_DEFAULT_WAIT, System.IntPtr.Zero);
        if (handle.Equals(IntPtr.Zero)) { throw new Win32Exception(); }
        return handle;
    }

    public static Transfer Receive(SafeHandleMinusOneIsInvalid handle) {
        if (NativeMethods.ConnectNamedPipe(handle, IntPtr.Zero) == false) { throw new Win32Exception(); }

        uint available = 0;
        while (available == 0) {
            uint bytesRead = 0, thismsg = 0;
            if (NativeMethods.PeekNamedPipe(handle, null, 0, ref bytesRead, ref available, ref thismsg) == false) {
                Thread.Sleep(100);
                available = 0;
            }
        }

        byte[] buffer = new byte[available];
        uint read = 0;
        NativeOverlapped overlapped = new NativeOverlapped();
        if (NativeMethods.ReadFile(handle, buffer, (uint)buffer.Length, ref read, ref overlapped) == false) { throw new Win32Exception(); }
        if (read != available) { throw new InvalidOperationException("Invalid byte count."); }

        return Transfer.Deserialize(buffer);
    }

    public static void Cancel(SafeHandleMinusOneIsInvalid handle) {
        NativeMethods.CancelIoEx(handle, IntPtr.Zero);
    }


    private static string NamedPipeName = @"\\.\pipe\JosipMedved-Seobiseu";


    internal static class NativeMethods {

        public const uint FILE_ATTRIBUTE_NORMAL = 0;
        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const int INVALID_HANDLE_VALUE = -1;
        public const uint NMPWAIT_USE_DEFAULT_WAIT = 0x00000000;
        public const uint OPEN_EXISTING = 3;
        public const uint PIPE_ACCESS_DUPLEX = 0x00000003;
        public const uint PIPE_READMODE_BYTE = 0x00000000;
        public const uint PIPE_TYPE_BYTE = 0x00000000;
        public const uint PIPE_UNLIMITED_INSTANCES = 255;
        public const uint PIPE_WAIT = 0x00000000;


        public class FileSafeHandle : SafeHandleMinusOneIsInvalid {

            public FileSafeHandle()
                : base(true) {
            }

            public FileSafeHandle(IntPtr preexistingHandle, bool ownsHandle)
                : base(ownsHandle) {
                SetHandle(preexistingHandle);
            }

            protected override bool ReleaseHandle() {
                if (handle != IntPtr.Zero) {
                    NativeMethods.CloseHandle(this.handle);
                    handle = IntPtr.Zero;
                    return true;
                }
                return false;
            }

            public override string ToString() {
                return this.handle.ToString();
            }

        }


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CancelIoEx(SafeHandle hFile, IntPtr lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ConnectNamedPipe(SafeHandleMinusOneIsInvalid hNamedPipe, IntPtr lpOverlapped);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern FileSafeHandle CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, System.IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, System.IntPtr hTemplateFile);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern FileSafeHandle CreateNamedPipe(string lpName, uint dwOpenMode, uint dwPipeMode, uint nMaxInstances, uint nOutBufferSize, uint nInBufferSize, uint nDefaultTimeOut, System.IntPtr lpSecurityAttributes);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekNamedPipe(SafeHandleMinusOneIsInvalid hNamedPipe, byte[] lpBuffer, uint nBufferSize, ref uint lpBytesRead, ref uint lpTotalBytesAvail, ref uint lpBytesLeftThisMessage);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReadFile(SafeHandleMinusOneIsInvalid hFile, byte[] lpBuffer, uint nNumberOfBytesToRead, ref uint lpNumberOfBytesRead, ref NativeOverlapped lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WriteFile(FileSafeHandle hFile, byte[] lpBuffer, uint nNumberOfBytesToWrite, ref uint lpNumberOfBytesWritten, ref NativeOverlapped lpOverlapped);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WaitNamedPipe(string lpNamedPipeName, uint nTimeOut);

    }

}
