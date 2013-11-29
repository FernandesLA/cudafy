﻿/*
CUDAfy.NET - LGPL 2.1 License
Please consider purchasing a commerical license - it helps development, frees you from LGPL restrictions
and provides you with support.  Thank you!
Copyright (C) 2011 Hybrid DSP Systems
http://www.hybriddsp.com

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cudafy.Host;
using Cudafy.UnitTests;
using Cudafy.Maths.RAND;
using NUnit.Framework;


namespace Cudafy.Maths.UnitTests
{
    public class CURANDHostTests : CudafyUnitTest, ICudafyUnitTest
    {
        private GPGPU _gpu;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _gpu = CudafyHost.GetDevice(CudafyModes.Target);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _gpu.FreeAll();
        }

        [Test]
        public void Test_CURAND_Device_Reference_Example()
        {
            int n = 100;
            float[] devData = _gpu.Allocate<float>(n);
            float[] hostData = new float[n];

            GPGPURAND gen = GPGPURAND.Create(_gpu, curandRngType.CURAND_RNG_PSEUDO_DEFAULT);
            gen.SetPseudoRandomGeneratorSeed(1234);
            gen.GenerateUniform(devData);
            
            _gpu.CopyFromDevice(devData, hostData);

            for (int i = 0; i < n; i++)
                Console.WriteLine(hostData[i]);

            gen.Dispose();
        }

        [Test]
        public void Test_CURAND_Host_Reference_Example()
        {
            int n = 100;    
            float[] hostData = new float[n];

            GPGPURAND gen = GPGPURAND.Create(_gpu, curandRngType.CURAND_RNG_PSEUDO_DEFAULT, true);
            gen.SetPseudoRandomGeneratorSeed(1234);
            gen.GenerateUniform(hostData);

            for (int i = 0; i < n; i++)
                Console.WriteLine(hostData[i]);

            gen.Dispose();
        }


        public void TestSetUp()
        {
            
        }

        public void TestTearDown()
        {
            
        }
    }
}
