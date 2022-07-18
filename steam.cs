using System;
using System.IO;
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace steam_checker
{
    public class utilities
    {
        [DllImport("kernel32.dll")]
        private static extern bool SetDllDirectory(string lpPathName);

        private static string sPath { get; set; }

        public static void debug(string text)
        {
            using (TextWriter tw = new StreamWriter(@"debug.log", true))
            {
                tw.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] " + text);
                tw.Close();
            }
        }

        public static void setPath()
        {
            if (IsSteamPresent())
            {
                SetDllDirectory(sPath);
                utilities.debug((new StackTrace(true)).GetFrame(0).GetMethod().Name + " Steam IS Present, Setting DLL Path, " + sPath);
            }
            else
            {
                utilities.debug((new StackTrace(true)).GetFrame(0).GetMethod().Name + " false?");
            }
        }

        static bool IsSteamPresent(){
            utilities.debug((new StackTrace(true)).GetFrame(0).GetMethod().Name + " triggered");
            RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Valve\\Steam", true);
            if(key != null){
                sPath = (string)key.GetValue("InstallPath");
                utilities.debug((new StackTrace(true)).GetFrame(0).GetMethod().Name + " returning true");
                return true;
            }else{
                utilities.debug((new StackTrace(true)).GetFrame(0).GetMethod().Name + " returning false");
                return false;
            }
        }
    }

    public class steam_plugin
    { 
        #region Steam DLL Call's
        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SteamStartEngine(ref TSteamError SteamErr);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SteamStartup(uint UsingMask, ref TSteamError SteamErr);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SteamLogin(string User, string Password, int isSecureComputer, ref TSteamError SteamErr);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SteamSetUser(string User, ref int UserSet, ref TSteamError SteamErr);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SteamProcessCall(uint SteamHandle, ref TSteamProgress Progress, ref TSteamError SteamError);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SteamIsLoggedIn(ref int isLoggedIn, ref TSteamError SteamErr);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SteamLogout(ref TSteamError SteamErr);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SteamCleanup(ref TSteamError SteamErr);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SteamGetAppStats(ref TSteamAppStats AppStats, ref TSteamError SteamErr);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SteamGetAppIds(uint[] AppIDs, uint MaxAppIDs, ref TSteamError SteamErr);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SteamEnumerateApp(uint Appid, ref TSteamApp ByvalAppStruc, ref TSteamError SteamErr);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SteamIsAppSubscribed(uint appid, ref uint IsAppSubscribed, ref uint IsAppReady, ref TSteamError SteamErr);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SteamShutdownEngine(ref TSteamError SteamErr);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern System.UInt32 SteamChangePassword(string CurrentPassphrase, string NewPassphrase, ref TSteamError SteamErr);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern System.UInt32 SteamChangeEmailAddress(string NewEmailAddress, ref TSteamError SteamErr);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern System.UInt32 SteamChangePersonalQA(string CurrentPassphrase, string NewPersonalQuestion, string NewAnswerToQuestion, ref TSteamError SteamErr);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern System.UInt32 SteamRequestAccountsByEmailAddressEmail(string Arg1, ref TSteamError SteamErr);

        [DllImport("steam.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern System.UInt32 SteamSubscribe(uint SubscriptionId, ref string TSteamUpdateStats, ref TSteamError SteamErr);

        #endregion

        #region Enumorators
        public enum eSteamError
        {
            eSteamErrorNone = 0,
            eSteamErrorUnknown = 1,
            eSteamErrorLibraryNotInitialized = 2,
            eSteamErrorLibraryAlreadyInitialized = 3,
            eSteamErrorConfig = 4,
            eSteamErrorContentServerConnect = 5,
            eSteamErrorBadHandle = 6,
            eSteamErrorHandlesExhausted = 7,
            eSteamErrorBadArg = 8,
            eSteamErrorNotFound = 9,
            eSteamErrorRead = 10,
            eSteamErrorEOF = 11,
            eSteamErrorSeek = 12,
            eSteamErrorCannotWriteNonUserConfigFile = 13,
            eSteamErrorCacheOpen = 14,
            eSteamErrorCacheRead = 15,
            eSteamErrorCacheCorrupted = 16,
            eSteamErrorCacheWrite = 17,
            eSteamErrorCacheSession = 18,
            eSteamErrorCacheInternal = 19,
            eSteamErrorCacheBadApp = 20,
            eSteamErrorCacheVersion = 21,
            eSteamErrorCacheBadFingerPrint = 22,
            eSteamErrorNotFinishedProcessing = 23,
            eSteamErrorNothingToDo = 24,
            eSteamErrorCorruptEncryptedUserIDTicket = 25,
            eSteamErrorSocketLibraryNotInitialized = 26,
            eSteamErrorFailedToConnectToUserIDTicketValidationServer = 27,
            eSteamErrorBadProtocolVersion = 28,
            eSteamErrorReplayedUserIDTicketFromClient = 29,
            eSteamErrorReceiveResultBufferTooSmall = 30,
            eSteamErrorSendFailed = 31,
            eSteamErrorReceiveFailed = 32,
            eSteamErrorReplayedReplyFromUserIDTicketValidationServer = 33,
            eSteamErrorBadSignatureFromUserIDTicketValidationServer = 34,
            eSteamErrorValidationStalledSoAborted = 35,
            eSteamErrorInvalidUserIDTicket = 36,
            eSteamErrorClientLoginRateTooHigh = 37,
            eSteamErrorClientWasNeverValidated = 38,
            eSteamErrorInternalSendBufferTooSmall = 39,
            eSteamErrorInternalReceiveBufferTooSmall = 40,
            eSteamErrorUserTicketExpired = 41,
            eSteamErrorCDKeyAlreadyInUseOnAnotherClient = 42,
            eSteamErrorNotLoggedIn = 101,
            eSteamErrorAlreadyExists = 102,
            eSteamErrorAlreadySubscribed = 103,
            eSteamErrorNotSubscribed = 104,
            eSteamErrorAccessDenied = 105,
            eSteamErrorFailedToCreateCacheFile = 106,
            eSteamErrorCallStalledSoAborted = 107,
            eSteamErrorEngineNotRunning = 108,
            eSteamErrorEngineConnectionLost = 109,
            eSteamErrorLoginFailed = 110,
            eSteamErrorAccountPending = 111,
            eSteamErrorCacheWasMissingRetry = 112,
            eSteamErrorLocalTimeIncorrect = 113,
            eSteamErrorAccountDisabled = 115,
            eSteamErrorNetwork = 200
        }
        public enum eDetailedPlatformErrorType
        {
            eNoDetailedErrorAvailable,
            eStandardCerrno,
            eWin32LastError,
            eWinSockLastError,
            eDetailedPlatformErrorCount
        }
        #endregion

        #region Structors
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct TSteamError
        {
            public eSteamError eSteamError;
            public eDetailedPlatformErrorType eDetailedErrorType;
            public uint ErrCode;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string ErrDescription;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct TSteamProgress
        {
            public int Valid;
            public int Percent;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string Progress;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct TSteamAppStats
        {
            public uint NumApps;
            public uint MaxNameChars;
            public uint MaxVersionLabelChars;
            public uint MaxLaunchOptions;
            public uint MaxLaunchOptionDescChars;
            public uint MaxLaunchOptionCmdLineChars;
            public uint MaxNumIcons;
            public uint MaxIconSize;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct TSteamApp
        {
            public string Name;
            public uint maxNameChars;
            public string latestVersionLabel;
            public uint maxLatestVersionLabelChars;
            public string currentVersionLabel;
            public uint maxCurrentVersionLabelChars;
            public string cacheFile;
            public uint maxCacheFileChars;
            public uint id;
            public uint latestVersionId;
            public uint currentVersionId;
            public uint minCacheFileSizeMB;
            public uint maxCacheFileSizeMB;
            public uint numLaunchOptions;
            public uint numIcons;
            public uint numVersions;
            public uint numDependencies;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct TSteamAppLaunchOption
        {
            public string description;
            public uint maxDescChars;
            public string cmdLine;
            public uint maxCmdLineChars;
            public uint index;
            public uint iconIndex;
            public uint noDesktopShortcut;
            public uint noStartMenuShortcut;
            public uint isLongRunningUnattended;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct TSteamSubscriptionStats
        {
            public uint NumSubscriptions;
            public uint MaxNameChars;
            public uint MaxApps;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct TSteamSubscription
        {
            public string Name;
            public uint MaxNameChars;
            public IntPtr AppIDs;
            public uint MaxAppIDs;
            public uint ID;
            public uint NumApps;
            public uint DurationInSeconds;
            public uint CostInCents;
            public uint IsAutoRenewing;
        }
        #endregion

        #region Objects
            private TSteamError SErr = new TSteamError();
            private TSteamProgress TProgress = new TSteamProgress();
        #endregion

        #region Variables
            private int iUserSet;
            private int isLoggedIn;
            private int iSteamReturn;          
            
            private uint sHandle;
            private uint startoptions = 15;
            private StackTrace st = new StackTrace();
        #endregion

        private void init()
        {
            utilities.setPath();
            iSteamReturn = SteamStartEngine(ref SErr);
            iSteamReturn = SteamStartup(startoptions, ref SErr);
            iSteamReturn = SteamIsLoggedIn(ref isLoggedIn, ref SErr);
            TProgress.Valid = 1;
        }

        private void cleanup()
        {
            iSteamReturn = SteamCleanup(ref SErr);
            iSteamReturn = SteamShutdownEngine(ref SErr);
            if (SteamIsLoggedIn(ref isLoggedIn, ref SErr) == 1)
            {
                sHandle = SteamLogout(ref SErr);
            }
            TProgress.Valid = 1;
            if (File.Exists("*.dmp"))
            {
                File.Delete("*.dmp");
            }
        }

        private void call(uint sHandle, string methodname, string debugmsg)
        {
            utilities.debug(methodname + " Calling " + debugmsg);
            while (SteamProcessCall(sHandle, ref TProgress, ref SErr) == 0)
            {
                Application.DoEvents();
            }
            Thread.Sleep(500);
            if (SErr.eSteamError != eSteamError.eSteamErrorNone) // Having an Issues
            {
                MessageBox.Show(SErr.ToString());
            }
        }

        public void checkAccount(string username, string password, bool icheckGames)
        {
            utilities.debug((new StackTrace(true)).GetFrame(1).GetMethod().Name + " checkacc");
            init();
            call(SteamLogin(username, password, 1, ref SErr), (new StackTrace(true)).GetFrame(0).GetMethod().Name, "SteamLogin");
            //Thread.Sleep(500);
            iSteamReturn = SteamIsLoggedIn(ref isLoggedIn, ref SErr);
            if (isLoggedIn == 1)
            {
                call(SteamSetUser(username, ref iUserSet, ref SErr), (new StackTrace(true)).GetFrame(0).GetMethod().Name, "SteamSetUser");
                if (iUserSet == 1)
                {
                    //Logging.Logrtb(Splash.frm.richTextBox1, "Account " + username + ":" + password + " Successful");
                    utilities.debug("-----------------------------------");
                    utilities.debug("Account " + username + ":" + password + " Successful");
                    utilities.debug("-----------------------------------");
                    if (icheckGames == true)
                    {
                        checkGames();
                    }
                }
                else
                {
                    if (SErr.eSteamError == eSteamError.eSteamErrorLoginFailed)
                    {
                        
                    }
                }

            }
            call(SteamLogout(ref SErr), (new StackTrace(true)).GetFrame(0).GetMethod().Name, "SteamLogout");
            cleanup();
        }

        public void loginAccount(string username, string password)
        {
            init(); 
            sHandle = SteamLogin(username, password, 1, ref SErr);
            while (SteamProcessCall(sHandle, ref TProgress, ref SErr) == 0)
            {
                Application.DoEvents();
            }
            if (SErr.eSteamError != eSteamError.eSteamErrorNone)
            {
                MessageBox.Show("Steam Error : " + SErr.ErrCode + " - " + SErr.ErrDescription + " - " + SErr.eDetailedErrorType + " - " + SErr.eSteamError, "Sorry, We have encountered an error : " + SErr.ErrDescription);
            }
            iSteamReturn = SteamIsLoggedIn(ref isLoggedIn, ref SErr);
            if (isLoggedIn == 1)
            {
                sHandle = SteamSetUser(username, ref iUserSet, ref SErr);
                TProgress.Valid = 1;
                while (SteamProcessCall(sHandle, ref TProgress, ref SErr) == 0)
                {
                    Application.DoEvents();
                }
                if (iUserSet == 1)
                {
                    MessageBox.Show("I am logged in under " + username); // We are now logged in
                }

            }
            sHandle = SteamLogout(ref SErr);
            TProgress.Valid = 1;

            while (SteamProcessCall(sHandle, ref TProgress, ref SErr) == 0)
            {
                Application.DoEvents();
            }
        }

        public void changePassword(string username, string password, string newpassword)
        {
            loginAccount(username, password);
            Thread.Sleep(2000);
            sHandle = SteamChangePassword(password, newpassword, ref SErr);
            while (SteamProcessCall(sHandle, ref TProgress, ref SErr) == 0)
            {
                Thread.Sleep(800);
            }
            if (SErr.eSteamError != eSteamError.eSteamErrorNone)
            {
                MessageBox.Show("Steam Error : " + SErr.ErrCode + " - " + SErr.ErrDescription + " - " + SErr.eDetailedErrorType + " - " + SErr.eSteamError, "Sorry, We have encountered an error : " + SErr.ErrDescription);
            }
            else
            {
                MessageBox.Show("password successfully changed");
            }
            cleanup();
        }

        private bool IsVerified()
        {
            return true;
        }

        public void checkGames()
        {
            if (isLoggedIn == 1)
            {

            }
        }
    }
}
