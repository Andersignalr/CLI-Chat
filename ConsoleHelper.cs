using System.Runtime.InteropServices;

public class ConsoleHelper
{

    [StructLayout(LayoutKind.Sequential)]

    struct POSITION
    {

        public short x;

        public short y;

    }



    // http://msdn.microsoft.com/en-us/library/ms682073

    [DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

    private static extern int GetStdHandle(int nStdHandle);



    [DllImport("kernel32.dll", EntryPoint = "SetConsoleCursorPosition", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

    private static extern int SetConsoleCursorPosition(int hConsoleOutput, POSITION dwCursorPosition);



    public void gotoxy(short x, short y)
    {

        const int STD_OUTPUT_HANDLE = -11;

        int hConsoleHandle = GetStdHandle(STD_OUTPUT_HANDLE);

        POSITION position;

        position.x = x;

        position.y = y;

        SetConsoleCursorPosition(hConsoleHandle, position);

    }



    [StructLayout(LayoutKind.Sequential)]

    struct CONSOLERECT
    {

        public short Left;

        public short Top;

        public short Right;

        public short Bottom;

    }



    [StructLayout(LayoutKind.Sequential)]

    struct CONSOLEBUFFER
    {

        public POSITION size;

        public POSITION position;

        public int attrib;

        public CONSOLERECT window;

        public POSITION maxsize;

    }



    [DllImport("kernel32.dll", EntryPoint = "FillConsoleOutputCharacter", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

    private static extern int FillConsoleOutputCharacter(int handleConsoleOutput, byte fillchar, int len, POSITION writecord, ref int numberofbyeswritten);



    [DllImport("kernel32.dll", EntryPoint = "GetConsoleScreenBufferInfo", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

    private static extern int GetConsoleScreenBufferInfo(int handleConsoleOutput, ref CONSOLEBUFFER bufferinfo);



    public void ClearScreen() // clrscr
    {

        const int STD_OUTPUT_HANDLE = -11;

        int hConsoleHandle = GetStdHandle(STD_OUTPUT_HANDLE);



        int hWrittenChars = 0;

        CONSOLEBUFFER strConsoleInfo = new CONSOLEBUFFER();

        POSITION pos;

        pos.x = pos.y = 0;

        GetConsoleScreenBufferInfo(hConsoleHandle, ref strConsoleInfo);

        FillConsoleOutputCharacter(hConsoleHandle, 32, strConsoleInfo.size.x * strConsoleInfo.size.y, pos, ref hWrittenChars);

        SetConsoleCursorPosition(hConsoleHandle, pos);

    }

}

