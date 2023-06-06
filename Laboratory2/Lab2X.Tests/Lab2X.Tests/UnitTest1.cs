using Lab2.ViewModels;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Maui.Devices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using FluentAssertions;
using Lab2.Services;
using NSubstitute;

namespace Lab2X.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Title_ShouldBeValid()
        {
            var viewModel = new MainViewModel();
            Assert.Equal("Welcome to.NET MAUI", viewModel.Title);
        }

        [Fact]
        public void Time_ShouldBeValid()
        {
            var viewModel = new MainViewModel();
            viewModel.CurrentDateTime.Should().NotBeNullOrEmpty();
            DateTime.TryParse(viewModel.CurrentDateTime, out var value).Should().BeTrue();
            value.Date.Ticks.Should().BeGreaterThan(0);
        }
    }
}