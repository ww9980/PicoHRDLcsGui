/******************************************************************************
*
* Filename: PicoHRDLImports.cs
*  
* Description:
*  This file contains .NET wrapper calls corresponding to function calls 
*  defined in the HRDL.h C header file. 
*  It also has the enums required by the (wrapped) function calls.
*   
* Copyright © 2015-2018 Pico Technology Ltd. See LICENSE file for terms.
*
******************************************************************************/

using System.Text;
using System.Runtime.InteropServices;

namespace PicoHRDLImports
{
    class Imports
    {

        #region Constants
        private const string _DRIVER_FILENAME = "picohrdl.dll";

        public const int MAX_VOLTAGE_RANGE = 2500;

        // Constants to define model
        public const int HRDL_ADC_20 = 20;
        public const int HRDL_ADC_24 = 24;

        #endregion

        #region Driver Enums

        public enum HRDLInputs : short
        {
            HRDL_DIGITAL_CHANNELS,
            HRDL_ANALOG_IN_CHANNEL_1,
            HRDL_ANALOG_IN_CHANNEL_2,
            HRDL_ANALOG_IN_CHANNEL_3,
            HRDL_ANALOG_IN_CHANNEL_4,
            HRDL_ANALOG_IN_CHANNEL_5,
            HRDL_ANALOG_IN_CHANNEL_6,
            HRDL_ANALOG_IN_CHANNEL_7,
            HRDL_ANALOG_IN_CHANNEL_8,
            HRDL_ANALOG_IN_CHANNEL_9,
            HRDL_ANALOG_IN_CHANNEL_10,
            HRDL_ANALOG_IN_CHANNEL_11,
            HRDL_ANALOG_IN_CHANNEL_12,
            HRDL_ANALOG_IN_CHANNEL_13,
            HRDL_ANALOG_IN_CHANNEL_14,
            HRDL_ANALOG_IN_CHANNEL_15,
            HRDL_ANALOG_IN_CHANNEL_16,
            HRDL_MAX_ANALOG_CHANNELS = HRDL_ANALOG_IN_CHANNEL_16
        } 

        public enum HRDLDigitalIOChannel : short
        {   
          HRDL_DIGITAL_IO_CHANNEL_1 = 0x01,
          HRDL_DIGITAL_IO_CHANNEL_2 = 0x02,
          HRDL_DIGITAL_IO_CHANNEL_3 = 0x04,
          HRDL_DIGITAL_IO_CHANNEL_4 = 0x08,
          HRDL_MAX_DIGITAL_CHANNELS = 4
        } 

        public enum HRDLRange : short
        {
            HRDL_2500_MV,
            HRDL_1250_MV,
            HRDL_625_MV,
            HRDL_313_MV,
            HRDL_156_MV,
            HRDL_78_MV,
            HRDL_39_MV,  
            HRDL_MAX_RANGES
        }	

        public enum HRDLConversionTime : short
        {
            HRDL_60MS,
            HRDL_100MS,
            HRDL_180MS,
            HRDL_340MS,
            HRDL_660MS,
            HRDL_MAX_CONVERSION_TIMES
        }	

        public enum HRDLInfo : short
        {
            HRDL_DRIVER_VERSION,
            HRDL_USB_VERSION,
            HRDL_HARDWARE_VERSION,
            HRDL_VARIANT_INFO,
            HRDL_BATCH_AND_SERIAL,
            HRDL_CAL_DATE,	
            HRDL_KERNEL_DRIVER_VERSION, 
            HRDL_ERROR,
            HRDL_SETTINGS,
        } 

        public enum HRDLErrorCode : short
        {
            HRDL_OK,
            HRDL_KERNEL_DRIVER,
            HRDL_NOT_FOUND,
            HRDL_CONFIG_FAIL,
            HRDL_ERROR_OS_NOT_SUPPORTED,
            HRDL_MAX_DEVICES
        } 

        public enum SettingsError : short
        {
	        SE_CONVERSION_TIME_OUT_OF_RANGE,
	        SE_SAMPLEINTERVAL_OUT_OF_RANGE,
	        SE_CONVERSION_TIME_TOO_SLOW,
	        SE_CHANNEL_NOT_AVAILABLE,
	        SE_INVALID_CHANNEL,
	        SE_INVALID_VOLTAGE_RANGE,
	        SE_INVALID_PARAMETER,
	        SE_CONVERSION_IN_PROGRESS,
	        SE_COMMUNICATION_FAILED,
	        SE_OK,
	        SE_MAX = SE_OK
        }

        public enum HRDLOpenProgress : short
        {
            HRDL_OPEN_PROGRESS_FAIL     = -1,
            HRDL_OPEN_PROGRESS_PENDING  = 0,
            HRDL_OPEN_PROGRESS_COMPLETE = 1
        }

        public enum BlockMethod : short
        {
            HRDL_BM_BLOCK,
            HRDL_BM_WINDOW,
            HRDL_BM_STREAM
        }

        public enum HRDLMainsRejection : short
        {
            HRDL_FIFTY_HERTZ,
            HRDL_SIXTY_HERTZ
        }

        #endregion

        #region Driver Imports

        [DllImport(_DRIVER_FILENAME, EntryPoint = "HRDLOpenUnit")]
        public static extern short HRDLOpenUnit();

        [DllImport(_DRIVER_FILENAME, EntryPoint = "HRDLCloseUnit")]
        public static extern short HRDLCloseUnit(short handle);

        [DllImport(_DRIVER_FILENAME, EntryPoint = "HRDLGetUnitInfo")]
        public static extern short GetUnitInfo(
            short handle,
            StringBuilder infoString,
            short stringLength,
            short info);

        [DllImport(_DRIVER_FILENAME, EntryPoint = "HRDLSetMains")]
        public static extern short SetMains(
            short handle,
            HRDLMainsRejection sixtyHertz);

        /// <summary>
        /// Enable or disable an analog channel
        /// </summary>
        /// <param name="handle">Handle识别符</param>
        /// <param name="channel">对于单端使用(short)Imports.HRDLInputs.HRDL_ANALOG_IN_CHANNEL_1</param>
        /// <param name="enabled">1=enabled，0=disabled</param>
        /// <param name="range">对于LEP使用(short)Imports.HRDLRange.HRDL_2500_MV</param>
        /// <param name="singleEnded">单端1，差分0</param>
        /// <returns></returns>
        [DllImport(_DRIVER_FILENAME, EntryPoint = "HRDLSetAnalogInChannel")]
        public static extern short SetAnalogInChannel(
            short handle,
            short channel,
            short enabled,
            short range,
            short singleEnded);

        [DllImport(_DRIVER_FILENAME, EntryPoint = "HRDLSetDigitalIOChannel")]
        public static extern short SetDigitalIOChannel(
            short handle,
            short directionOut,
            short digitalOutPinState,
            short enabledDigitalIn);

        /// <summary>
        /// Set sample interval
        /// </summary>
        /// <param name="handle">Handle</param>
        /// <param name="sampleInterval_ms">以ms单位计的间隔。
        /// 应大于conversion time x 启用中通道数。
        /// </param>
        /// <param name="conversionTime">conversion time，必须是以下数值：
        /// 0 HRDL_60MS
        /// 1 HRDL_100MS
        /// 2 HRDL_180MS
        /// 3 HRDL_340MS
        /// 4 HRDL_660MS
        /// </param>
        /// <returns></returns>
        [DllImport(_DRIVER_FILENAME, EntryPoint = "HRDLSetInterval")]
        public static extern short SetInterval(
            short handle, 
            int sampleInterval_ms, 
            short conversionTime);

        /// <summary>
        /// ADC读数中的max min
        /// </summary>
        /// <param name="handle">Handle</param>
        /// <param name="minAdc">*指针min</param>
        /// <param name="maxAdc">*指针max</param>
        /// <param name="channel">哪一个channel</param>
        /// <returns></returns>
        [DllImport(_DRIVER_FILENAME, EntryPoint = "HRDLGetMinMaxAdcCounts")]
        public static extern short GetMinMaxAdcCounts(
            short handle,
            out int minAdc,
            out int maxAdc,
            short channel);

        /// <summary>
        /// 返回启用中的channel数目
        /// </summary>
        /// <param name="handle">Handle</param>
        /// <param name="nEnabledChannels">结果变量指针</param>
        /// <returns>1成功0失败</returns>
        [DllImport(_DRIVER_FILENAME, EntryPoint = "HRDLGetNumberOfEnabledChannels")]
        public static extern short GetNumberOfEnabledChannels(
            short handle,
            out short nEnabledChannels);

        [DllImport(_DRIVER_FILENAME, EntryPoint = "HRDLGetSingleValue")]
        public static extern short GetSingleValue(
            short handle,
            short channel,
            short range,
            short conversionTime,
            short singleEnded,
            out short overflow,
            out int value);

        /// <summary>
        /// 获取时间戳和读数，时间戳从本次读取记，是n x interval
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="times"></param>
        /// <param name="values"></param>
        /// <param name="overflow"></param>
        /// <param name="noOfValues"></param>
        /// <returns></returns>
        [DllImport(_DRIVER_FILENAME, EntryPoint = "HRDLGetTimesAndValues")]
        public static extern short GetTimesAndValues(
            short handle,
            int[] times,
            int[] values,
            out short overflow,
            int noOfValues);

        /// <summary>
        /// 返回读数
        /// </summary>
        /// <param name="handle">Handle</param>
        /// <param name="values">int32_t * 指针写结果。举例2channel的结果格式如下
        /// sample no 0 1 2 3 4 5
        /// channel   1 2 1 2 1 2
        /// 若有digital input，1前有DI结果。</param>
        /// <param name="overflow">int16* 若任何input超出了max voltage，此处将有high bit</param>
        /// <param name="noOfValues">每个活动channel中取得的sample数量</param>
        /// <returns>0失败，其他成功</returns>
        [DllImport(_DRIVER_FILENAME, EntryPoint = "HRDLGetValues")]
        public static extern short GetValues(
            short handle,
            int[] values,
            out short overflow,
            int noOfValues);


        /// <summary>
        /// 查看driver是否已ready送出reading
        /// </summary>
        /// <param name="handle">Handle</param>
        /// <returns>1 = ready，0 = 没有</returns>
        [DllImport(_DRIVER_FILENAME, EntryPoint = "HRDLReady")]
        public static extern short HRDLReady(
            short handle);

        /// <summary>
        /// 使用Streaming方法读取时候的开始步。读取的数据将存在driver的buffer中
        /// </summary>
        /// <param name="handle">Handle</param>
        /// <param name="nValues">每个active channel的取样数量，直到执行GetValues或GetTimeAndValues。
        /// 最少要10
        /// </param>
        /// <param name="method">Stream = 2，Block = 0，Window = 1</param>
        /// <returns></returns>
        [DllImport(_DRIVER_FILENAME, EntryPoint = "HRDLRun")]
        public static extern short HRDLRun(
            short handle,
            int nValues,
            short method);

        /// <summary>
        /// 当使用Streaming或Windowed模式，使用此方法停止collection
        /// 当使用Block模式，此方法强制停止collection
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        [DllImport(_DRIVER_FILENAME, EntryPoint = "HRDLStop")]
        public static extern short HRDLStop(
            short handle);

        #endregion

    }
}
