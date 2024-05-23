using System.Runtime.InteropServices;

namespace PCSC
{
    class Api
    {
        [DllImport("winscard.dll")]
        public static extern uint SCardEstablishContext(uint dwScope, IntPtr pvReserved1, IntPtr pvReserved2, out IntPtr phContext);

        [DllImport("winscard.dll", EntryPoint = "SCardListReadersW", CharSet = CharSet.Unicode)]
        public static extern uint SCardListReaders(
          IntPtr hContext, byte[]? mszGroups, byte[]? mszReaders, ref UInt32 pcchReaders);

        [DllImport("winscard.dll")]
        public static extern uint SCardReleaseContext(IntPtr phContext);

        [DllImport("winscard.dll", EntryPoint = "SCardConnectW", CharSet = CharSet.Unicode)]
        public static extern uint SCardConnect(IntPtr hContext, string szReader,
             uint dwShareMode, uint dwPreferredProtocols, ref IntPtr phCard,
             ref IntPtr pdwActiveProtocol);

        [DllImport("winscard.dll")]
        public static extern uint SCardDisconnect(IntPtr hCard, int Disposition);

        [StructLayout(LayoutKind.Sequential)]
        internal class SCARD_IO_REQUEST
        {
            internal uint dwProtocol;
            internal int cbPciLength;
            public SCARD_IO_REQUEST()
            {
                dwProtocol = 0;
            }
        }

        [DllImport("winscard.dll")]
        public static extern uint SCardTransmit(IntPtr hCard, IntPtr pioSendRequest, byte[] SendBuff, int SendBuffLen, SCARD_IO_REQUEST pioRecvRequest,
                byte[] RecvBuff, ref int RecvBuffLen);

        [DllImport("winscard.dll")]
        public static extern uint SCardControl(IntPtr hCard, int controlCode, byte[] inBuffer, int inBufferLen, byte[] outBuffer, int outBufferLen, ref int bytesReturned);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SCARD_READERSTATE
        {
            internal string szReader;
            internal IntPtr pvUserData;
            internal UInt32 dwCurrentState;
            internal UInt32 dwEventState;
            internal UInt32 cbAtr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
            internal byte[] rgbAtr;
        }

        [DllImport("winscard.dll", EntryPoint = "SCardGetStatusChangeW", CharSet = CharSet.Unicode)]
        public static extern uint SCardGetStatusChange(IntPtr hContext, int dwTimeout, [In, Out] SCARD_READERSTATE[] rgReaderStates, int cReaders);

        [DllImport("winscard.dll")]
        public static extern int SCardStatus(IntPtr hCard, string szReader, ref int cch, ref int state, ref int protocol, ref byte[] bAttr, ref int cByte);


        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll")]
        public static extern void FreeLibrary(IntPtr handle);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr handle, string procName);
    }

    class Constant
    {
        public const uint SCARD_S_SUCCESS = 0;
        public const uint SCARD_E_NO_SERVICE = 0x8010001D;
        public const uint SCARD_E_TIMEOUT = 0x8010000A;

        public const uint SCARD_SCOPE_USER = 0;
        public const uint SCARD_SCOPE_TERMINAL = 1;
        public const uint SCARD_SCOPE_SYSTEM = 2;

        public const int SCARD_STATE_UNAWARE = 0x0000;
        public const int SCARD_STATE_CHANGED = 0x00000002;
        public const int SCARD_STATE_PRESENT = 0x00000020;
        public const UInt32 SCARD_STATE_EMPTY = 0x00000010;
        public const int SCARD_SHARE_SHARED = 0x00000002;
        public const int SCARD_SHARE_EXCLUSIVE = 0x00000001;
        public const int SCARD_SHARE_DIRECT = 0x00000003;

        public const int SCARD_PROTOCOL_T0 = 1;
        public const int SCARD_PROTOCOL_T1 = 2;
        public const int SCARD_PROTOCOL_RAW = 4;

        public const int SCARD_LEAVE_CARD = 0;
        public const int SCARD_RESET_CARD = 1;
        public const int SCARD_UNPOWER_CARD = 2;
        public const int SCARD_EJECT_CARD = 3;

        // SCardStatus status values
        public const int SCARD_UNKNOWN = 0x00000000;
        public const int SCARD_ABSENT = 0x00000001;
        public const int SCARD_PRESENT = 0x00000002;
        public const int SCARD_SWALLOWED = 0x00000003;
        public const int SCARD_POWERED = 0x00000004;
        public const int SCARD_NEGOTIABLE = 0x00000005;
        public const int SCARD_SPECIFICMODE = 0x00000006;
    }
}