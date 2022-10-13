using PicoHRDLImports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PicoHRDLGui
{
    internal class HRDLthread
    {
        public int minAdc = 0;
        public int maxAdc = 0;
        public Tuple<float[], float[]> startLive(short handle, int numDataPnts)
        {
            if (handle > 0)
            {
                // 使用Channel 1，2500mV量程，差分信号
                short analogChannelStatus = Imports.SetAnalogInChannel(handle,
                                                                       (short)Imports.HRDLInputs.HRDL_ANALOG_IN_CHANNEL_1,
                                                                       1,
                                                                       (short)Imports.HRDLRange.HRDL_2500_MV,
                                                                       0);
                // Set Interval time = 80 ms, conversion time = 60 ms
                short returnIntervalStatus = Imports.SetInterval(handle, 80, (short)Imports.HRDLConversionTime.HRDL_60MS);
                // Run方法
                short status = Imports.HRDLRun(handle, numDataPnts, (short)Imports.BlockMethod.HRDL_BM_STREAM);


                return collectLive(handle, numDataPnts);
            }
            else
            {
                return null;
            }
        }

        public Tuple<float[], float[]> collectLive(short handle, int numDataPnts)
        {
            if (handle > 0)
            {
                short ready = Imports.HRDLReady(handle);

                while (ready != 1)
                {
                    ready = Imports.HRDLReady(handle);
                    Thread.Sleep(100);
                }
                short overflow = 0;
                int[] time = new int[numDataPnts];
                int[] data = new int[numDataPnts];

                int numDataCollected = Imports.GetTimesAndValues(handle, time, data, out overflow, numDataPnts);

                

                short returnAdcMaxMin = Imports.GetMinMaxAdcCounts(handle,
                                                                   out minAdc,
                                                                   out maxAdc,
                                                                   (short)Imports.HRDLInputs.HRDL_ANALOG_IN_CHANNEL_1);

                float[] scaledData = new float[numDataCollected];
                float[] scaledTime = new float[numDataCollected];

                for (int n = 0; n < numDataCollected; n++)
                {
                    scaledTime[n] = time[n];
                    scaledData[n] = adcToMv(data[n], (short)Imports.HRDLRange.HRDL_2500_MV, maxAdc);
                }
                return Tuple.Create(scaledTime, scaledData);
            }
            else
            {
                return null;
            }
        }

        public int stopLive(short handle)
        {
            if (handle > 0)
            {
                short stopStatus = Imports.HRDLStop(handle);
                return 1;
            }
            else 
            {
                return 0;
            }
        }

        public float adcToMv(int value, short range, int maxValue)
        {
            float mvValue = 0.0f;

            float vMax = (float)(Imports.MAX_VOLTAGE_RANGE / Math.Pow(2, range)); // Find voltage scaling factor

            mvValue = ((float)value / maxValue) * vMax;

            return mvValue;
        }
    }
}
