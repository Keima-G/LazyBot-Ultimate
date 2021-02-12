namespace LazyLib.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Principal;
    using System.Text;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public static class Memory
    {
        private const int DEFAULT_MEMORY_SIZE = 0x1000;
        public const byte ASCII_CHAR_LENGTH = 1;
        public const byte UNICODE_CHAR_LENGTH = 2;
        public const int DEFAULT_MemORY_SIZE = 0x1000;
        private static readonly UTF8Encoding UTF8 = new UTF8Encoding();

        public static uint AllocateMemory() => 
            AllocateMemory(0x1000);

        public static uint AllocateMemory(int nSize) => 
            AllocateMemory(nSize, 0x1000, 0x40);

        public static uint AllocateMemory(int nSize, uint dwAllocationType, uint dwProtect) => 
            AllocateMemory(nSize, 0, dwAllocationType, dwProtect);

        public static uint AllocateMemory(int nSize, uint dwAddress, uint dwAllocationType, uint dwProtect) => 
            VirtualAllocEx(ProcessHandle, dwAddress, nSize, dwAllocationType, dwProtect);

        [SuppressUnmanagedCodeSecurity, DllImport("kernel32.dll", SetLastError=true)]
        internal static extern bool CloseHandle(IntPtr hHandle);
        public static void CloseProcess()
        {
            CloseHandle(ProcessHandle);
            Process.LeaveDebugMode();
        }

        public static bool FreeMemory(uint dwAddress) => 
            FreeMemory(dwAddress, 0, 0x8000);

        public static bool FreeMemory(uint dwAddress, int nSize, uint dwFreeType)
        {
            if (dwFreeType == 0x8000)
            {
                nSize = 0;
            }
            return VirtualFreeEx(ProcessHandle, dwAddress, nSize, dwFreeType);
        }

        public static bool OpenProcess(int processId)
        {
            if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                Process.EnterDebugMode();
            }
            ProcessObject = Process.GetProcessById(processId);
            ProcessHandle = OpenProcess(0x1f0fff, false, processId);
            return (ProcessHandle != IntPtr.Zero);
        }

        [SuppressUnmanagedCodeSecurity, DllImport("kernel32.dll")]
        internal static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        public static T Read<T>(params IntPtr[] addresses)
        {
            if (addresses.Length != 0)
            {
                if (addresses.Length == 1)
                {
                    return ReadInternal<T>((uint) ((int) addresses[0]));
                }
                uint num = 0;
                for (int i = 0; i < addresses.Length; i++)
                {
                    if (i == (addresses.Length - 1))
                    {
                        return ReadInternal<T>(((uint) ((int) addresses[i])) + num);
                    }
                    num = ReadInternal<uint>(num + ((uint) ((int) addresses[i])));
                }
            }
            return default(T);
        }

        public static T Read<T>(params uint[] addresses)
        {
            if (addresses.Length != 0)
            {
                if (addresses.Length == 1)
                {
                    return ReadInternal<T>(addresses[0]);
                }
                uint num = 0;
                for (int i = 0; i < addresses.Length; i++)
                {
                    if (i == (addresses.Length - 1))
                    {
                        return ReadInternal<T>(addresses[i] + num);
                    }
                    num = ReadInternal<uint>(num + addresses[i]);
                }
            }
            return default(T);
        }

        public static string ReadAsciiString(uint dwAddress, int nLength)
        {
            string str;
            IntPtr zero = IntPtr.Zero;
            try
            {
                int nSize = nLength;
                zero = Marshal.AllocHGlobal((int) (nSize + 1));
                Marshal.WriteByte(zero, nLength, 0);
                if (ReadRawMemory(ProcessHandle, dwAddress, zero, nSize) != nSize)
                {
                    throw new Exception();
                }
                str = Marshal.PtrToStringAnsi(zero);
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (zero != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(zero);
                }
            }
            return str;
        }

        public static byte[] ReadBytes(uint address, int count)
        {
            int num;
            byte[] lpBuffer = new byte[count];
            return ((!ReadProcessMemory(ProcessHandle, address, lpBuffer, count, out num) || (num != count)) ? null : lpBuffer);
        }

        public static byte[] ReadBytes(IntPtr hProcess, uint dwAddress, int nSize)
        {
            byte[] buffer;
            IntPtr zero = IntPtr.Zero;
            try
            {
                zero = Marshal.AllocHGlobal(nSize);
                int length = ReadRawMemory(hProcess, dwAddress, zero, nSize);
                if (length != nSize)
                {
                    throw new Exception("ReadProcessMemory error in ReadBytes");
                }
                buffer = new byte[length];
                Marshal.Copy(zero, buffer, 0, length);
            }
            catch
            {
                return null;
            }
            finally
            {
                if (zero != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(zero);
                }
            }
            return buffer;
        }

        private static T ReadInternal<T>(uint address)
        {
            T local;
            try
            {
                if (ReferenceEquals(typeof(T), typeof(string)))
                {
                    List<char> list = new List<char>();
                    uint num = 0;
                    while (true)
                    {
                        uint[] addresses = new uint[] { address + num };
                        char item = Read<byte>(addresses);
                        if (item == '\0')
                        {
                            local = (T) new string(list.ToArray());
                            break;
                        }
                        num++;
                        list.Add(item);
                    }
                }
                else
                {
                    int count = Marshal.SizeOf(typeof(T));
                    if (ReferenceEquals(typeof(T), typeof(IntPtr)))
                    {
                        local = (T) ((IntPtr) BitConverter.ToInt64(ReadBytes(address, count), 0));
                    }
                    else
                    {
                        object obj2;
                        switch (Type.GetTypeCode(typeof(T)))
                        {
                            case TypeCode.Boolean:
                                obj2 = ReadBytes(address, 1)[0] != 0;
                                break;

                            case TypeCode.Char:
                                obj2 = (char) ReadBytes(address, 1)[0];
                                break;

                            case TypeCode.SByte:
                                obj2 = (sbyte) ReadBytes(address, count)[0];
                                break;

                            case TypeCode.Byte:
                                obj2 = ReadBytes(address, count)[0];
                                break;

                            case TypeCode.Int16:
                                obj2 = BitConverter.ToInt16(ReadBytes(address, count), 0);
                                break;

                            case TypeCode.UInt16:
                                obj2 = BitConverter.ToUInt16(ReadBytes(address, count), 0);
                                break;

                            case TypeCode.Int32:
                                obj2 = BitConverter.ToInt32(ReadBytes(address, count), 0);
                                break;

                            case TypeCode.UInt32:
                                obj2 = BitConverter.ToUInt32(ReadBytes(address, count), 0);
                                break;

                            case TypeCode.Int64:
                                obj2 = BitConverter.ToInt64(ReadBytes(address, count), 0);
                                break;

                            case TypeCode.UInt64:
                                obj2 = BitConverter.ToUInt64(ReadBytes(address, count), 0);
                                break;

                            case TypeCode.Single:
                                obj2 = BitConverter.ToSingle(ReadBytes(address, count), 0);
                                break;

                            case TypeCode.Double:
                                obj2 = BitConverter.ToDouble(ReadBytes(address, count), 0);
                                break;

                            default:
                            {
                                IntPtr destination = Marshal.AllocHGlobal(count);
                                Marshal.Copy(ReadBytes(address, count), 0, destination, count);
                                obj2 = Marshal.PtrToStructure(destination, typeof(T));
                                Marshal.FreeHGlobal(destination);
                                break;
                            }
                        }
                        local = (T) obj2;
                    }
                }
            }
            catch
            {
                local = default(T);
            }
            return local;
        }

        public static object ReadObject(uint dwAddress, Type objType)
        {
            object obj2;
            IntPtr zero = IntPtr.Zero;
            try
            {
                int cb = Marshal.SizeOf(objType);
                zero = Marshal.AllocHGlobal(cb);
                if (ReadRawMemory(ProcessHandle, dwAddress, zero, cb) != cb)
                {
                    throw new Exception("ReadProcessMemory error in ReadObject.");
                }
                obj2 = Marshal.PtrToStructure(zero, objType);
            }
            catch
            {
                return null;
            }
            finally
            {
                if (zero != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(zero);
                }
            }
            return obj2;
        }

        [SuppressUnmanagedCodeSecurity, DllImport("kernel32.dll", SetLastError=true)]
        internal static extern bool ReadProcessMemory(IntPtr hProcess, uint lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);
        [DllImport("kernel32")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, uint dwAddress, IntPtr lpBuffer, int nSize, out int lpBytesRead);
        public static int ReadRawMemory(IntPtr hProcess, uint dwAddress, IntPtr lpBuffer, int nSize)
        {
            try
            {
                int num;
                if (!ReadProcessMemory(hProcess, dwAddress, lpBuffer, nSize, out num))
                {
                    throw new Exception("ReadProcessMemory failed");
                }
                return num;
            }
            catch
            {
                return 0;
            }
        }

        public static T ReadRelative<T>(params uint[] addresses)
        {
            if (addresses.Length >= 1)
            {
                addresses[0] += BaseAddress;
            }
            return Read<T>(addresses);
        }

        public static T ReadStruct<T>(IntPtr address) where T: struct
        {
            IntPtr destination = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));
            Marshal.Copy(ReadBytes((uint) ((int) address), Marshal.SizeOf(typeof(T))), 0, destination, Marshal.SizeOf(typeof(T)));
            T local = (T) Marshal.PtrToStructure(destination, typeof(T));
            Marshal.FreeHGlobal(destination);
            return local;
        }

        public static T ReadStruct<T>(uint address) where T: struct
        {
            IntPtr destination = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));
            Marshal.Copy(ReadBytes(address, Marshal.SizeOf(typeof(T))), 0, destination, Marshal.SizeOf(typeof(T)));
            T local = (T) Marshal.PtrToStructure(destination, typeof(T));
            Marshal.FreeHGlobal(destination);
            return local;
        }

        public static string ReadUtf8(uint dwAddress, int Size)
        {
            byte[] bytes = ReadBytes(ProcessHandle, dwAddress, Size);
            return ((bytes != null) ? UTF8Decoder(bytes) : string.Empty);
        }

        public static string ReadUtf8StringRelative(uint address, int length)
        {
            address += BaseAddress;
            byte[] bytes = ReadBytes(ProcessHandle, address, length);
            if (bytes == null)
            {
                return "";
            }
            string str = Encoding.UTF8.GetString(bytes);
            if (str.IndexOf('\0') != -1)
            {
                str = str.Remove(str.IndexOf('\0'));
            }
            return str;
        }

        private static string UTF8Decoder(byte[] bytes)
        {
            string str = UTF8.GetString(bytes, 0, bytes.Length);
            if (str.IndexOf("\0") != -1)
            {
                str = str.Remove(str.IndexOf("\0"), str.Length - str.IndexOf("\0"));
            }
            return str;
        }

        [DllImport("kernel32")]
        public static extern uint VirtualAllocEx(IntPtr hProcess, uint dwAddress, int nSize, uint dwAllocationType, uint dwProtect);
        [DllImport("kernel32")]
        public static extern bool VirtualFreeEx(IntPtr hProcess, uint dwAddress, int nSize, uint dwFreeType);
        public static unsafe void Write<T>(uint address, T value)
        {
            if (value is string)
            {
                WriteBytes(address, Encoding.ASCII.GetBytes(value as string));
            }
            else
            {
                int length = Marshal.SizeOf(value);
                byte* numPtr = stackalloc byte[(IntPtr) length];
                Marshal.StructureToPtr(value, (IntPtr) numPtr, true);
                byte[] destination = new byte[length];
                Marshal.Copy((IntPtr) numPtr, destination, 0, length);
                WriteBytes(address, destination);
            }
        }

        public static int WriteBytes(uint address, byte[] val)
        {
            int num;
            if (!WriteProcessMemory(ProcessHandle, address, val, (uint) val.Length, out num))
            {
                throw new Exception($"Could not write the specified bytes! {address:X8} [{Marshal.GetLastWin32Error()}]");
            }
            return num;
        }

        [SuppressUnmanagedCodeSecurity, DllImport("kernel32.dll", SetLastError=true)]
        internal static extern bool WriteProcessMemory(IntPtr hProcess, uint lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        private static Process ProcessObject { get; set; }

        public static int ProcessId =>
            ProcessObject.Id;

        public static IntPtr WindowHandle =>
            ProcessObject.MainWindowHandle;

        public static ProcessModule MainModule
        {
            get
            {
                ProcessModule mainModule;
                try
                {
                    mainModule = ProcessObject.MainModule;
                }
                catch
                {
                    return null;
                }
                return mainModule;
            }
        }

        public static uint BaseAddress =>
            (uint) ((int) MainModule.BaseAddress);

        public static IntPtr ProcessHandle { get; private set; }

        public static bool Initialized =>
            ProcessHandle != IntPtr.Zero;

        public static class MemoryAllocType
        {
            public const uint MEM_COMMIT = 0x1000;
            public const uint MEM_RESERVE = 0x2000;
            public const uint MEM_RESET = 0x80000;
            public const uint MEM_PHYSICAL = 0x400000;
            public const uint MEM_TOP_DOWN = 0x100000;
        }

        public static class MemoryFreeType
        {
            public const uint MEM_DECOMMIT = 0x4000;
            public const uint MEM_RELEASE = 0x8000;
        }

        public static class MemoryProtectType
        {
            public const uint PAGE_EXECUTE = 0x10;
            public const uint PAGE_EXECUTE_READ = 0x20;
            public const uint PAGE_EXECUTE_READWRITE = 0x40;
            public const uint PAGE_EXECUTE_WRITECOPY = 0x80;
            public const uint PAGE_NOACCESS = 1;
            public const uint PAGE_READONLY = 2;
            public const uint PAGE_READWRITE = 4;
            public const uint PAGE_WRITECOPY = 8;
            public const uint PAGE_GUARD = 0x100;
            public const uint PAGE_NOCACHE = 0x200;
            public const uint PAGE_WRITECOMBINE = 0x400;
        }
    }
}

