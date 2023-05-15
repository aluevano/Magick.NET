﻿// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheInverseLevelMethod
    {
        [Fact]
        public void ShouldUseCompositeAsDefaultChannels()
        {
            using var first = new MagickImage(Files.MagickNETIconPNG);
            using var second = first.Clone();
            first.InverseLevel(new Percentage(50), new Percentage(10));
            second.InverseLevel(new Percentage(50), new Percentage(10), Channels.Composite);

            Assert.Equal(first.Signature, second.Signature);
        }

        [Fact]
        public void ShouldUseOneAsGammaDefault()
        {
            using var first = new MagickImage(Files.MagickNETIconPNG);
            using var second = first.Clone();
            first.InverseLevel(new Percentage(50), new Percentage(10));
            second.InverseLevel(new Percentage(50), new Percentage(10), 1.0, Channels.Composite);

            Assert.Equal(first.Signature, second.Signature);
        }

        [Fact]
        public void ShouldScaleTheColors()
        {
            using var first = new MagickImage(Files.MagickNETIconPNG);
            using var second = first.Clone();
            first.InverseLevel(new Percentage(50.0), new Percentage(10.0));

            var fifty = (QuantumType)(Quantum.Max * 0.5);
            var ten = (QuantumType)(Quantum.Max * 0.1);
            second.InverseLevel(fifty, ten, Channels.Red);
            second.InverseLevel(fifty, ten, Channels.Green | Channels.Blue);
            second.InverseLevel(fifty, ten, Channels.Alpha);

            Assert.Equal(first.Signature, second.Signature);
        }
    }
}
