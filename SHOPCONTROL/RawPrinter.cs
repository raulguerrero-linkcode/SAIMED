using System;
using System.IO;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
public class RawPrinter
{

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    struct DOCINFOW
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pDocName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pOutputFile;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pDataType;
    }


    //[DllImport("winspool.Drv", EntryPoint = "OpenPrinterW",
    //   CharSet = CharSet.Unicode, SetLastError = true,
    //   ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    //static extern bool OpenPrinter(string src, ref IntPtr hPrinter, long pd);

    [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA",
        SetLastError = true, CharSet = CharSet.Ansi,
        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    private static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, Int32 pDefault);

    [DllImport("winspool.Drv", EntryPoint = "ClosePrinter",
       CharSet = CharSet.Unicode, SetLastError = true,
       ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    static extern bool ClosePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterW",
       CharSet = CharSet.Unicode, SetLastError = true,
       ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    static extern bool StartDocPrinter(IntPtr hPrinter,
                         int level, ref DOCINFOW pDI);

    [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter",
       CharSet = CharSet.Unicode, SetLastError = true,
       ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    static extern bool EndDocPrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter",
       CharSet = CharSet.Unicode, SetLastError = true,
       ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    static extern bool StartPagePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter",
       CharSet = CharSet.Unicode, SetLastError = true,
       ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    static extern bool EndPagePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "WritePrinter",
       CharSet = CharSet.Unicode, SetLastError = true,
       ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    static extern bool WritePrinter(IntPtr hPrinter,
           IntPtr pBytes, int dwCount, ref int dwWritten);


    //SendBytesToPrinter()
    //When the function is given a printer name and an unmanaged array of
    //bytes, the function sends those bytes to the print queue.
    //Returns True on success or False on failure.
    public bool SendBytesToPrinter(string szPrinterName,
                             IntPtr pBytes, int dwCount)
    {
        // The printer handle.
        IntPtr hPrinter = new IntPtr(0);
        // Last error - in case there was trouble.
        int dwError;
        // Describes your document (name, port, data type).
        DOCINFOW di = new DOCINFOW();
        // The number of bytes written by WritePrinter().
        int dwWritten = 0;
        // Your success code.
        bool bSuccess;

        // Set up the DOCINFO structure.
        di.pDocName = "My C# .NET RAW Document";
        di.pDataType = "RAW";
        // Assume failure unless you specifically succeed.
        bSuccess = false;
        if (OpenPrinter(szPrinterName, out hPrinter, 0))
        {
            if (StartDocPrinter(hPrinter, 1, ref di))
            {
                if (StartPagePrinter(hPrinter))
                {
                    // Write your printer-specific bytes to the printer.
                    bSuccess = WritePrinter(hPrinter, pBytes,
                                     dwCount, ref dwWritten);
                    EndPagePrinter(hPrinter);
                }
                EndDocPrinter(hPrinter);
            }
            ClosePrinter(hPrinter);
        }
        // If you did not succeed, GetLastError may give more information
        // about why not.
        if (bSuccess == false)
        {
            dwError = Marshal.GetLastWin32Error();
        }
        return bSuccess;
    }


    // SendFileToPrinter()
    // When the function is given a file name and a printer name, 
    // the function reads the contents of the file and sends the
    // contents to the printer.
    // Presumes that the file contains printer-ready data.
    // Shows how to use the SendBytesToPrinter function.
    // Returns True on success or False on failure.
    public bool SendFileToPrinter(string szPrinterName, string szFileName)
    {
        // Open the file.
        FileStream fs = new FileStream(szFileName, FileMode.Open);
        // Create a BinaryReader on the file.
        BinaryReader br = new BinaryReader(fs);
        // Dim an array of bytes large enough to hold the file's contents.
        byte[] bytes = new byte[fs.Length];
        bool bSuccess;
        // Your unmanaged pointer
        IntPtr pUnmanagedBytes;

        // Read the contents of the file into the array.
        bytes = br.ReadBytes(Convert.ToInt32(fs.Length));
        // Allocate some unmanaged memory for those bytes.
        pUnmanagedBytes = Marshal.AllocCoTaskMem(Convert.ToInt32(fs.Length));
        // Copy the managed byte array into the unmanaged array.
        Marshal.Copy(bytes, 0, pUnmanagedBytes, Convert.ToInt32(fs.Length));
        // Send the unmanaged bytes to the printer.
        bSuccess = SendBytesToPrinter(szPrinterName,
                   pUnmanagedBytes, Convert.ToInt32(fs.Length));
        // Free the unmanaged memory that you allocated earlier.
        Marshal.FreeCoTaskMem(pUnmanagedBytes);
        return bSuccess;
    }

    public bool SendStreamToPrinter(string szPrinterName, Stream szstream)
    {
        // Create a BinaryReader on the file.
        BinaryReader br = new BinaryReader(szstream);
        // Dim an array of bytes large enough to hold the file's contents.
        byte[] bytes = new byte[szstream.Length];
        bool bSuccess;
        // Your unmanaged pointer
        IntPtr pUnmanagedBytes;

        // Read the contents of the file into the array.
        bytes = br.ReadBytes(Convert.ToInt32(szstream.Length));
        // Allocate some unmanaged memory for those bytes.
        pUnmanagedBytes = Marshal.AllocCoTaskMem(Convert.ToInt32(szstream.Length));
        // Copy the managed byte array into the unmanaged array.
        Marshal.Copy(bytes, 0, pUnmanagedBytes, Convert.ToInt32(szstream.Length));
        // Send the unmanaged bytes to the printer.
        bSuccess = SendBytesToPrinter(szPrinterName,
                   pUnmanagedBytes, Convert.ToInt32(szstream.Length));
        // Free the unmanaged memory that you allocated earlier.
        Marshal.FreeCoTaskMem(pUnmanagedBytes);
        return bSuccess;
    }

    // When the function is given a string and a printer name,
    // the function sends the string to the printer as raw bytes.
    public bool SendStringToPrinter(string szPrinterName, string szString)
    {
        bool bSuccess;
        IntPtr pBytes = new IntPtr(0);
        int dwCount;
        //How many characters are in the string?
        dwCount = szString.Length;
        //Assume that the printer is expecting ANSI text, and then convert
        //the string to ANSI text.
        pBytes = Marshal.StringToCoTaskMemAnsi(szString);
        //Send the converted ANSI string to the printer.
        bSuccess = SendBytesToPrinter(szPrinterName, pBytes, dwCount);

        Marshal.FreeCoTaskMem(pBytes);
        return bSuccess;
    }
}