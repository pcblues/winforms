﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Tests;

// NB: doesn't require thread affinity
public class DataGridViewCellStyleTests
{
    [Fact]
    public void DataGridViewCellStyle_Ctor_Default()
    {
        DataGridViewCellStyle style = new();
        Assert.Equal(DataGridViewCellStyleScopes.None, style.Scope);
        Assert.Equal(DataGridViewContentAlignment.NotSet, style.Alignment);
        Assert.Equal(Color.Empty, style.BackColor);
        Assert.Equal(DBNull.Value, style.DataSourceNullValue);
        Assert.Null(style.Font);
        Assert.Equal(Color.Empty, style.ForeColor);
        Assert.Empty(style.Format);
        Assert.Equal(CultureInfo.CurrentCulture, style.FormatProvider);
        Assert.True(style.IsDataSourceNullValueDefault);
        Assert.True(style.IsFormatProviderDefault);
        Assert.True(style.IsNullValueDefault);
        Assert.Equal(string.Empty, style.NullValue);
        Assert.Equal(Padding.Empty, style.Padding);
        Assert.Equal(Color.Empty, style.SelectionBackColor);
        Assert.Equal(Color.Empty, style.SelectionForeColor);
        Assert.Null(style.Tag);
        Assert.Equal(DataGridViewTriState.NotSet, style.WrapMode);
    }

    [Fact]
    public void DataGridViewCellStyle_Ctor_NonEmptyDataGridViewCellStyle_Success()
    {
        NumberFormatInfo formatProvider = new();
        DataGridViewCellStyle source = new()
        {
            Alignment = DataGridViewContentAlignment.BottomCenter,
            BackColor = Color.Red,
            DataSourceNullValue = "dbNull",
            Font = SystemFonts.DefaultFont,
            ForeColor = Color.Blue,
            Format = "format",
            FormatProvider = formatProvider,
            NullValue = "null",
            Padding = new Padding(1, 2, 3, 4),
            SelectionBackColor = Color.Green,
            SelectionForeColor = Color.Yellow,
            Tag = "tag",
            WrapMode = DataGridViewTriState.True
        };
        DataGridViewCellStyle style = new(source);

        Assert.Equal(DataGridViewCellStyleScopes.None, style.Scope);
        Assert.Equal(DataGridViewContentAlignment.BottomCenter, style.Alignment);
        Assert.Equal(Color.Red, style.BackColor);
        Assert.Equal("dbNull", style.DataSourceNullValue);
        Assert.Equal(SystemFonts.DefaultFont, style.Font);
        Assert.Equal(Color.Blue, style.ForeColor);
        Assert.Equal("format", style.Format);
        Assert.Equal(formatProvider, style.FormatProvider);
        Assert.False(style.IsDataSourceNullValueDefault);
        Assert.False(style.IsFormatProviderDefault);
        Assert.False(style.IsNullValueDefault);
        Assert.Equal("null", style.NullValue);
        Assert.Equal(new Padding(1, 2, 3, 4), style.Padding);
        Assert.Equal(Color.Green, style.SelectionBackColor);
        Assert.Equal(Color.Yellow, style.SelectionForeColor);
        Assert.Equal("tag", style.Tag);
        Assert.Equal(DataGridViewTriState.True, style.WrapMode);
    }

    [Fact]
    public void DataGridViewCellStyle_Ctor_EmptyDataGridViewCellStyle_Success()
    {
        DataGridViewCellStyle source = new();
        DataGridViewCellStyle style = new(source);

        Assert.Equal(DataGridViewCellStyleScopes.None, style.Scope);
        Assert.Equal(DataGridViewContentAlignment.NotSet, style.Alignment);
        Assert.Equal(Color.Empty, style.BackColor);
        Assert.Equal(DBNull.Value, style.DataSourceNullValue);
        Assert.Null(style.Font);
        Assert.Equal(Color.Empty, style.ForeColor);
        Assert.Empty(style.Format);
        Assert.Equal(CultureInfo.CurrentCulture, style.FormatProvider);
        Assert.True(style.IsDataSourceNullValueDefault);
        Assert.True(style.IsFormatProviderDefault);
        Assert.True(style.IsNullValueDefault);
        Assert.Equal(string.Empty, style.NullValue);
        Assert.Equal(Padding.Empty, style.Padding);
        Assert.Equal(Color.Empty, style.SelectionBackColor);
        Assert.Equal(Color.Empty, style.SelectionForeColor);
        Assert.Null(style.Tag);
        Assert.Equal(DataGridViewTriState.NotSet, style.WrapMode);
    }

    [Fact]
    public void DataGridViewCellStyle_Ctor_NullDataGridViewCellStyle_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>("dataGridViewCellStyle", () => new DataGridViewCellStyle(null));
    }

    [Theory]
    [EnumData<DataGridViewContentAlignment>]
    public void DataGridViewCellStyle_Alignment_Set_GetReturnsExpected(DataGridViewContentAlignment value)
    {
        DataGridViewCellStyle style = new()
        {
            Alignment = value
        };
        Assert.Equal(value, style.Alignment);

        // Set same.
        style.Alignment = value;
        Assert.Equal(value, style.Alignment);
    }

    [Theory]
    [InvalidEnumData<DataGridViewContentAlignment>]
    public void DataGridViewCellStyle_Alignment_SetInvalid_ThrowsInvalidEnumArgumentException(DataGridViewContentAlignment value)
    {
        DataGridViewCellStyle style = new();
        Assert.Throws<InvalidEnumArgumentException>("value", () => style.Alignment = value);
    }

    [Theory]
    [CommonMemberData(typeof(CommonTestHelper), nameof(CommonTestHelper.GetColorWithEmptyTheoryData))]
    public void DataGridViewCellStyle_BackColor_Set_GetReturnsExpected(Color value)
    {
        DataGridViewCellStyle style = new()
        {
            BackColor = value
        };
        Assert.Equal(value, style.BackColor);

        // Set same.
        style.BackColor = value;
        Assert.Equal(value, style.BackColor);
    }

    [Fact]
    public void DataGridViewCellStyle_BackColor_SetEmpty_GetReturnsExpected()
    {
        DataGridViewCellStyle style = new()
        {
            BackColor = Color.Red
        };
        style.BackColor = Color.Empty;
        Assert.Equal(Color.Empty, style.BackColor);
    }

    public static IEnumerable<object[]> DataSourceNullValue_TestData()
    {
        yield return new object[] { DBNull.Value, true };
        yield return new object[] { null, false };
        yield return new object[] { string.Empty, false };
        yield return new object[] { "value", false };
        yield return new object[] { new AlwaysEqual(), false };
    }

    [Theory]
    [MemberData(nameof(DataSourceNullValue_TestData))]
    public void DataGridViewCellStyle_DataSourceNullValue_Set_GetReturnsExpected(object value, bool expectedIsDataSourceNullValueDefault)
    {
        DataGridViewCellStyle style = new()
        {
            DataSourceNullValue = value
        };
        Assert.Equal(value, style.DataSourceNullValue);
        Assert.Equal(expectedIsDataSourceNullValueDefault, style.IsDataSourceNullValueDefault);

        // Set same.
        style.DataSourceNullValue = value;
        Assert.Equal(value, style.DataSourceNullValue);
        Assert.Equal(expectedIsDataSourceNullValueDefault, style.IsDataSourceNullValueDefault);
    }

    [Theory]
    [MemberData(nameof(DataSourceNullValue_TestData))]
    public void DataGridViewCellStyle_DataSourceNullValue_SetWithNonNullOldValue_GetReturnsExpected(object value, bool expectedIsDataSourceNullValueDefault)
    {
        DataGridViewCellStyle style = new()
        {
            DataSourceNullValue = "oldValue"
        };
        style.DataSourceNullValue = value;
        Assert.Equal(value, style.DataSourceNullValue);
        Assert.Equal(expectedIsDataSourceNullValueDefault, style.IsDataSourceNullValueDefault);

        // Set same.
        style.DataSourceNullValue = value;
        Assert.Equal(value, style.DataSourceNullValue);
        Assert.Equal(expectedIsDataSourceNullValueDefault, style.IsDataSourceNullValueDefault);
    }

    [Theory]
    [MemberData(nameof(DataSourceNullValue_TestData))]
    public void DataGridViewCellStyle_DataSourceNullValue_SetWithNullOldValue_GetReturnsExpected(object value, bool expectedIsDataSourceNullValueDefault)
    {
        DataGridViewCellStyle style = new()
        {
            DataSourceNullValue = null
        };
        style.DataSourceNullValue = value;
        Assert.Equal(value, style.DataSourceNullValue);
        Assert.Equal(expectedIsDataSourceNullValueDefault, style.IsDataSourceNullValueDefault);

        // Set same.
        style.DataSourceNullValue = value;
        Assert.Equal(value, style.DataSourceNullValue);
        Assert.Equal(expectedIsDataSourceNullValueDefault, style.IsDataSourceNullValueDefault);
    }

    [Fact]
    public void DataGridViewCellStyle_DataSourceNullValue_SetEqual_GetReturnsExpected()
    {
        AlwaysEqual value = new();
        DataGridViewCellStyle style = new()
        {
            DataSourceNullValue = value
        };
        style.DataSourceNullValue = new AlwaysEqual();
        Assert.Same(value, style.DataSourceNullValue);
        Assert.False(style.IsDataSourceNullValueDefault);
    }

    [Fact]
    public void DataGridViewCellStyle_DataSourceNullValue_SetDifferent_GetReturnsExpected()
    {
        AlwaysEqual value = new();
        DataGridViewCellStyle style = new()
        {
            DataSourceNullValue = value
        };
        style.DataSourceNullValue = "value";
        Assert.Equal("value", style.DataSourceNullValue);
        Assert.False(style.IsDataSourceNullValueDefault);
    }

    [Fact]
    public void DataGridViewCellStyle_DataSourceNullValue_SetDBNull_GetReturnsExpected()
    {
        DataGridViewCellStyle style = new()
        {
            DataSourceNullValue = "value"
        };
        style.DataSourceNullValue = DBNull.Value;
        Assert.Equal(DBNull.Value, style.DataSourceNullValue);
        Assert.True(style.IsDataSourceNullValueDefault);
    }

    public static IEnumerable<object[]> Font_TestData()
    {
        yield return new object[] { null };
        yield return new object[] { SystemFonts.DefaultFont };
    }

    [Theory]
    [MemberData(nameof(Font_TestData))]
    public void DataGridViewCellStyle_Font_Set_GetReturnsExpected(Font value)
    {
        DataGridViewCellStyle style = new()
        {
            Font = value
        };
        Assert.Equal(value, style.Font);

        // Set same.
        style.Font = value;
        Assert.Equal(value, style.Font);
    }

    [Fact]
    public void DataGridViewCellStyle_Font_SetNull_GetReturnsExpected()
    {
        DataGridViewCellStyle style = new()
        {
            Font = SystemFonts.DefaultFont
        };
        style.Font = null;
        Assert.Null(style.Font);
    }

    [Theory]
    [CommonMemberData(typeof(CommonTestHelper), nameof(CommonTestHelper.GetColorTheoryData))]
    public void DataGridViewCellStyle_ForeColor_Set_GetReturnsExpected(Color value)
    {
        DataGridViewCellStyle style = new()
        {
            ForeColor = value
        };
        Assert.Equal(value, style.ForeColor);

        // Set same.
        style.ForeColor = value;
        Assert.Equal(value, style.ForeColor);
    }

    [Fact]
    public void DataGridViewCellStyle_ForeColor_SetEmpty_GetReturnsExpected()
    {
        DataGridViewCellStyle style = new()
        {
            ForeColor = Color.Red
        };
        style.ForeColor = Color.Empty;
        Assert.Equal(Color.Empty, style.ForeColor);
    }

    [Theory]
    [NormalizedStringData]
    public void DataGridViewCellStyle_Format_Set_GetReturnsExpected(string value, string expected)
    {
        DataGridViewCellStyle style = new()
        {
            Format = value
        };
        Assert.Equal(expected, style.Format);

        // Set same.
        style.Format = value;
        Assert.Equal(expected, style.Format);
    }

    [Theory]
    [NormalizedStringData]
    public void DataGridViewCellStyle_Format_SetWithNonNullOldValue_GetReturnsExpected(string value, string expected)
    {
        DataGridViewCellStyle style = new()
        {
            Format = "value"
        };
        style.Format = value;
        Assert.Equal(expected, style.Format);
    }

    public static IEnumerable<object[]> FormatProvider_TestData()
    {
        yield return new object[] { null, CultureInfo.CurrentCulture, true };

        NumberFormatInfo numberFormatInfo = new();
        yield return new object[] { numberFormatInfo, numberFormatInfo, false };
    }

    [Theory]
    [MemberData(nameof(FormatProvider_TestData))]
    public void DataGridViewCellStyle_FormatProvider_Set_GetReturnsExpected(IFormatProvider value, IFormatProvider expected, bool expectedIsFormatProviderDefault)
    {
        DataGridViewCellStyle style = new()
        {
            FormatProvider = value
        };
        Assert.Equal(expected, style.FormatProvider);
        Assert.Equal(expectedIsFormatProviderDefault, style.IsFormatProviderDefault);

        // Set same.
        style.FormatProvider = value;
        Assert.Equal(expected, style.FormatProvider);
        Assert.Equal(expectedIsFormatProviderDefault, style.IsFormatProviderDefault);
    }

    [Fact]
    public void DataGridViewCellStyle_FormatProvider_SetNull_GetReturnsExpected()
    {
        DataGridViewCellStyle style = new()
        {
            FormatProvider = new NumberFormatInfo()
        };
        style.FormatProvider = null;
        Assert.Equal(CultureInfo.CurrentCulture, style.FormatProvider);
        Assert.True(style.IsFormatProviderDefault);
    }

    public static IEnumerable<object[]> NullValue_TestData()
    {
        yield return new object[] { DBNull.Value, false };
        yield return new object[] { null, false };
        yield return new object[] { string.Empty, true };
        yield return new object[] { "value", false };
        yield return new object[] { new AlwaysEqual(), false };
    }

    [Theory]
    [MemberData(nameof(NullValue_TestData))]
    public void DataGridViewCellStyle_NullValue_Set_GetReturnsExpected(object value, bool expectedIsNullValueDefault)
    {
        DataGridViewCellStyle style = new()
        {
            NullValue = value
        };
        Assert.Equal(value, style.NullValue);
        Assert.Equal(expectedIsNullValueDefault, style.IsNullValueDefault);

        // Set same.
        style.NullValue = value;
        Assert.Equal(value, style.NullValue);
        Assert.Equal(expectedIsNullValueDefault, style.IsNullValueDefault);
    }

    [Theory]
    [MemberData(nameof(NullValue_TestData))]
    public void DataGridViewCellStyle_NullValue_SetWithNonNullOldValue_GetReturnsExpected(object value, bool expectedIsNullValueDefault)
    {
        DataGridViewCellStyle style = new()
        {
            NullValue = "oldValue"
        };
        style.NullValue = value;
        Assert.Equal(value, style.NullValue);
        Assert.Equal(expectedIsNullValueDefault, style.IsNullValueDefault);

        // Set same.
        style.NullValue = value;
        Assert.Equal(value, style.NullValue);
        Assert.Equal(expectedIsNullValueDefault, style.IsNullValueDefault);
    }

    [Theory]
    [MemberData(nameof(NullValue_TestData))]
    public void DataGridViewCellStyle_NullValue_SetWithNullOldValue_GetReturnsExpected(object value, bool expectedIsNullValueDefault)
    {
        DataGridViewCellStyle style = new()
        {
            NullValue = null
        };
        style.NullValue = value;
        Assert.Equal(value, style.NullValue);
        Assert.Equal(expectedIsNullValueDefault, style.IsNullValueDefault);

        // Set same.
        style.NullValue = value;
        Assert.Equal(value, style.NullValue);
        Assert.Equal(expectedIsNullValueDefault, style.IsNullValueDefault);
    }

    [Fact]
    public void DataGridViewCellStyle_NullValue_SetEqual_GetReturnsExpected()
    {
        AlwaysEqual value = new();
        DataGridViewCellStyle style = new()
        {
            NullValue = value
        };
        style.NullValue = new AlwaysEqual();
        Assert.Same(value, style.NullValue);
        Assert.False(style.IsNullValueDefault);
    }

    [Fact]
    public void DataGridViewCellStyle_NullValue_SetDifferent_GetReturnsExpected()
    {
        AlwaysEqual value = new();
        DataGridViewCellStyle style = new()
        {
            NullValue = value
        };
        style.NullValue = "value";
        Assert.Equal("value", style.NullValue);
        Assert.False(style.IsNullValueDefault);
    }

    [Fact]
    public void DataGridViewCellStyle_NullValue_SetEmpty_GetReturnsExpected()
    {
        DataGridViewCellStyle style = new()
        {
            NullValue = "value"
        };
        style.NullValue = string.Empty;
        Assert.Equal(string.Empty, style.NullValue);
        Assert.True(style.IsNullValueDefault);
    }

    public static IEnumerable<object[]> Padding_TestData()
    {
        yield return new object[] { Padding.Empty, Padding.Empty };
        yield return new object[] { new Padding(-1), Padding.Empty };
        yield return new object[] { new Padding(-2), Padding.Empty };
        yield return new object[] { new Padding(2), new Padding(2) };
        yield return new object[] { new Padding(1, 2, 3, 4), new Padding(1, 2, 3, 4) };
        yield return new object[] { new Padding(-1, 2, 3, 4), new Padding(0, 2, 3, 4) };
        yield return new object[] { new Padding(1, -2, 3, 4), new Padding(1, 0, 3, 4) };
        yield return new object[] { new Padding(1, 2, -3, 4), new Padding(1, 2, 0, 4) };
        yield return new object[] { new Padding(1, 2, 3, -4), new Padding(1, 2, 3, 0) };
    }

    [Theory]
    [MemberData(nameof(Padding_TestData))]
    public void DataGridViewCellStyle_Padding_Set_GetReturnsExpected(Padding value, Padding expected)
    {
        DataGridViewCellStyle style = new()
        {
            Padding = value
        };
        Assert.Equal(expected, style.Padding);

        // Set same.
        style.Padding = value;
        Assert.Equal(expected, style.Padding);
    }

    [Theory]
    [CommonMemberData(typeof(CommonTestHelper), nameof(CommonTestHelper.GetColorWithEmptyTheoryData))]
    public void DataGridViewCellStyle_SelectionBackColor_Set_GetReturnsExpected(Color value)
    {
        DataGridViewCellStyle style = new()
        {
            SelectionBackColor = value
        };
        Assert.Equal(value, style.SelectionBackColor);

        // Set same.
        style.SelectionBackColor = value;
        Assert.Equal(value, style.SelectionBackColor);
    }

    [Fact]
    public void DataGridViewCellStyle_SelectionBackColor_SetEmpty_GetReturnsExpected()
    {
        DataGridViewCellStyle style = new()
        {
            SelectionBackColor = Color.Red
        };
        style.SelectionBackColor = Color.Empty;
        Assert.Equal(Color.Empty, style.SelectionBackColor);
    }

    [Theory]
    [CommonMemberData(typeof(CommonTestHelper), nameof(CommonTestHelper.GetColorTheoryData))]
    public void DataGridViewCellStyle_SelectionForeColor_Set_GetReturnsExpected(Color value)
    {
        DataGridViewCellStyle style = new()
        {
            SelectionForeColor = value
        };
        Assert.Equal(value, style.SelectionForeColor);

        // Set same.
        style.SelectionForeColor = value;
        Assert.Equal(value, style.SelectionForeColor);
    }

    [Fact]
    public void DataGridViewCellStyle_SelectionForeColor_SetEmpty_GetReturnsExpected()
    {
        DataGridViewCellStyle style = new()
        {
            SelectionForeColor = Color.Red
        };
        style.SelectionForeColor = Color.Empty;
        Assert.Equal(Color.Empty, style.SelectionForeColor);
    }

    [Theory]
    [StringWithNullData]
    public void DataGridViewCellStyle_Tag_Set_GetReturnsExpected(object value)
    {
        DataGridViewCellStyle style = new()
        {
            Tag = value
        };
        Assert.Same(value, style.Tag);

        // Set same.
        style.Tag = value;
        Assert.Same(value, style.Tag);
    }

    [Theory]
    [StringWithNullData]
    public void DataGridViewCellStyle_Tag_SetWithNonNullOldValue_GetReturnsExpected(object value)
    {
        DataGridViewCellStyle style = new()
        {
            Tag = "tag"
        };
        style.Tag = value;
        Assert.Equal(value, style.Tag);

        // Set same.
        style.Tag = value;
        Assert.Equal(value, style.Tag);
    }

    [Theory]
    [EnumData<DataGridViewTriState>]
    public void DataGridViewCellStyle_WrapMode_Set_GetReturnsExpected(DataGridViewTriState value)
    {
        DataGridViewCellStyle style = new()
        {
            WrapMode = value
        };
        Assert.Equal(value, style.WrapMode);

        // Set same.
        style.WrapMode = value;
        Assert.Equal(value, style.WrapMode);
    }

    [Theory]
    [InvalidEnumData<DataGridViewTriState>]
    public void DataGridViewCellStyle_WrapMode_SetInvalid_ThrowsInvalidEnumArgumentException(DataGridViewTriState value)
    {
        DataGridViewCellStyle style = new();
        Assert.Throws<InvalidEnumArgumentException>("value", () => style.WrapMode = value);
    }

    [Fact]
    public void DataGridViewCellStyle_ApplyStyle_NonEmptyDataGridViewCellStyle_Success()
    {
        NumberFormatInfo formatProvider = new();
        DataGridViewCellStyle source = new()
        {
            Alignment = DataGridViewContentAlignment.BottomCenter,
            BackColor = Color.Red,
            DataSourceNullValue = "dbNull",
            Font = SystemFonts.DefaultFont,
            ForeColor = Color.Blue,
            Format = "format",
            FormatProvider = formatProvider,
            NullValue = "null",
            Padding = new Padding(1, 2, 3, 4),
            SelectionBackColor = Color.Green,
            SelectionForeColor = Color.Yellow,
            Tag = "tag",
            WrapMode = DataGridViewTriState.True
        };
        DataGridViewCellStyle style = new();
        style.ApplyStyle(source);

        Assert.Equal(DataGridViewCellStyleScopes.None, style.Scope);
        Assert.Equal(DataGridViewContentAlignment.BottomCenter, style.Alignment);
        Assert.Equal(Color.Red, style.BackColor);
        Assert.Equal("dbNull", style.DataSourceNullValue);
        Assert.Equal(SystemFonts.DefaultFont, style.Font);
        Assert.Equal(Color.Blue, style.ForeColor);
        Assert.Equal("format", style.Format);
        Assert.Equal(formatProvider, style.FormatProvider);
        Assert.False(style.IsDataSourceNullValueDefault);
        Assert.False(style.IsFormatProviderDefault);
        Assert.False(style.IsNullValueDefault);
        Assert.Equal("null", style.NullValue);
        Assert.Equal(new Padding(1, 2, 3, 4), style.Padding);
        Assert.Equal(Color.Green, style.SelectionBackColor);
        Assert.Equal(Color.Yellow, style.SelectionForeColor);
        Assert.Equal("tag", style.Tag);
        Assert.Equal(DataGridViewTriState.True, style.WrapMode);
    }

    [Fact]
    public void DataGridViewCellStyle_ApplyStyle_EmptyDataGridViewCellStyle_Nop()
    {
        NumberFormatInfo formatProvider = new();
        DataGridViewCellStyle source = new();
        DataGridViewCellStyle style = new()
        {
            Alignment = DataGridViewContentAlignment.BottomCenter,
            BackColor = Color.Red,
            DataSourceNullValue = "dbNull",
            Font = SystemFonts.DefaultFont,
            ForeColor = Color.Blue,
            Format = "format",
            FormatProvider = formatProvider,
            NullValue = "null",
            Padding = new Padding(1, 2, 3, 4),
            SelectionBackColor = Color.Green,
            SelectionForeColor = Color.Yellow,
            Tag = "tag",
            WrapMode = DataGridViewTriState.True
        };
        style.ApplyStyle(source);

        Assert.Equal(DataGridViewCellStyleScopes.None, style.Scope);
        Assert.Equal(DataGridViewContentAlignment.BottomCenter, style.Alignment);
        Assert.Equal(Color.Red, style.BackColor);
        Assert.Equal("dbNull", style.DataSourceNullValue);
        Assert.Equal(SystemFonts.DefaultFont, style.Font);
        Assert.Equal(Color.Blue, style.ForeColor);
        Assert.Equal("format", style.Format);
        Assert.Equal(formatProvider, style.FormatProvider);
        Assert.False(style.IsDataSourceNullValueDefault);
        Assert.False(style.IsFormatProviderDefault);
        Assert.False(style.IsNullValueDefault);
        Assert.Equal("null", style.NullValue);
        Assert.Equal(new Padding(1, 2, 3, 4), style.Padding);
        Assert.Equal(Color.Green, style.SelectionBackColor);
        Assert.Equal(Color.Yellow, style.SelectionForeColor);
        Assert.Equal("tag", style.Tag);
        Assert.Equal(DataGridViewTriState.True, style.WrapMode);
    }

    [Fact]
    public void DataGridViewCellStyle_ApplyStyle_NullDataGridViewCellStyle_ThrowsArgumentNullException()
    {
        DataGridViewCellStyle style = new();
        Assert.Throws<ArgumentNullException>("dataGridViewCellStyle", () => style.ApplyStyle(null));
    }

    [Fact]
    public void DataGridViewCellStyle_Clone_NonEmptyDataGridViewCellStyle_Success()
    {
        NumberFormatInfo formatProvider = new();
        DataGridViewCellStyle source = new()
        {
            Alignment = DataGridViewContentAlignment.BottomCenter,
            BackColor = Color.Red,
            DataSourceNullValue = "dbNull",
            Font = SystemFonts.DefaultFont,
            ForeColor = Color.Blue,
            Format = "format",
            FormatProvider = formatProvider,
            NullValue = "null",
            Padding = new Padding(1, 2, 3, 4),
            SelectionBackColor = Color.Green,
            SelectionForeColor = Color.Yellow,
            Tag = "tag",
            WrapMode = DataGridViewTriState.True
        };
        DataGridViewCellStyle style = source.Clone();

        Assert.Equal(DataGridViewCellStyleScopes.None, style.Scope);
        Assert.Equal(DataGridViewContentAlignment.BottomCenter, style.Alignment);
        Assert.Equal(Color.Red, style.BackColor);
        Assert.Equal("dbNull", style.DataSourceNullValue);
        Assert.Equal(SystemFonts.DefaultFont, style.Font);
        Assert.Equal(Color.Blue, style.ForeColor);
        Assert.Equal("format", style.Format);
        Assert.Equal(formatProvider, style.FormatProvider);
        Assert.False(style.IsDataSourceNullValueDefault);
        Assert.False(style.IsFormatProviderDefault);
        Assert.False(style.IsNullValueDefault);
        Assert.Equal("null", style.NullValue);
        Assert.Equal(new Padding(1, 2, 3, 4), style.Padding);
        Assert.Equal(Color.Green, style.SelectionBackColor);
        Assert.Equal(Color.Yellow, style.SelectionForeColor);
        Assert.Equal("tag", style.Tag);
        Assert.Equal(DataGridViewTriState.True, style.WrapMode);
    }

    [Fact]
    public void DataGridViewCellStyle_Clone_EmptyDataGridViewCellStyle_Success()
    {
        DataGridViewCellStyle source = new();
        DataGridViewCellStyle style = source.Clone();

        Assert.Equal(DataGridViewCellStyleScopes.None, style.Scope);
        Assert.Equal(DataGridViewContentAlignment.NotSet, style.Alignment);
        Assert.Equal(Color.Empty, style.BackColor);
        Assert.Equal(DBNull.Value, style.DataSourceNullValue);
        Assert.Null(style.Font);
        Assert.Equal(Color.Empty, style.ForeColor);
        Assert.Empty(style.Format);
        Assert.Equal(CultureInfo.CurrentCulture, style.FormatProvider);
        Assert.True(style.IsDataSourceNullValueDefault);
        Assert.True(style.IsFormatProviderDefault);
        Assert.True(style.IsNullValueDefault);
        Assert.Equal(string.Empty, style.NullValue);
        Assert.Equal(Padding.Empty, style.Padding);
        Assert.Equal(Color.Empty, style.SelectionBackColor);
        Assert.Equal(Color.Empty, style.SelectionForeColor);
        Assert.Null(style.Tag);
        Assert.Equal(DataGridViewTriState.NotSet, style.WrapMode);
    }

    [Fact]
    public void DataGridViewCellStyle_ICloneableClone_NonEmptyDataGridViewCellStyle_Success()
    {
        NumberFormatInfo formatProvider = new();
        ICloneable source = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.BottomCenter,
            BackColor = Color.Red,
            DataSourceNullValue = "dbNull",
            Font = SystemFonts.DefaultFont,
            ForeColor = Color.Blue,
            Format = "format",
            FormatProvider = formatProvider,
            NullValue = "null",
            Padding = new Padding(1, 2, 3, 4),
            SelectionBackColor = Color.Green,
            SelectionForeColor = Color.Yellow,
            Tag = "tag",
            WrapMode = DataGridViewTriState.True
        };
        DataGridViewCellStyle style = Assert.IsType<DataGridViewCellStyle>(source.Clone());

        Assert.Equal(DataGridViewCellStyleScopes.None, style.Scope);
        Assert.Equal(DataGridViewContentAlignment.BottomCenter, style.Alignment);
        Assert.Equal(Color.Red, style.BackColor);
        Assert.Equal("dbNull", style.DataSourceNullValue);
        Assert.Equal(SystemFonts.DefaultFont, style.Font);
        Assert.Equal(Color.Blue, style.ForeColor);
        Assert.Equal("format", style.Format);
        Assert.Equal(formatProvider, style.FormatProvider);
        Assert.False(style.IsDataSourceNullValueDefault);
        Assert.False(style.IsFormatProviderDefault);
        Assert.False(style.IsNullValueDefault);
        Assert.Equal("null", style.NullValue);
        Assert.Equal(new Padding(1, 2, 3, 4), style.Padding);
        Assert.Equal(Color.Green, style.SelectionBackColor);
        Assert.Equal(Color.Yellow, style.SelectionForeColor);
        Assert.Equal("tag", style.Tag);
        Assert.Equal(DataGridViewTriState.True, style.WrapMode);
    }

    [Fact]
    public void DataGridViewCellStyle_ICloneableClone_EmptyDataGridViewCellStyle_Success()
    {
        ICloneable source = new DataGridViewCellStyle();
        DataGridViewCellStyle style = Assert.IsType<DataGridViewCellStyle>(source.Clone());

        Assert.Equal(DataGridViewCellStyleScopes.None, style.Scope);
        Assert.Equal(DataGridViewContentAlignment.NotSet, style.Alignment);
        Assert.Equal(Color.Empty, style.BackColor);
        Assert.Equal(DBNull.Value, style.DataSourceNullValue);
        Assert.Null(style.Font);
        Assert.Equal(Color.Empty, style.ForeColor);
        Assert.Empty(style.Format);
        Assert.Equal(CultureInfo.CurrentCulture, style.FormatProvider);
        Assert.True(style.IsDataSourceNullValueDefault);
        Assert.True(style.IsFormatProviderDefault);
        Assert.True(style.IsNullValueDefault);
        Assert.Equal(string.Empty, style.NullValue);
        Assert.Equal(Padding.Empty, style.Padding);
        Assert.Equal(Color.Empty, style.SelectionBackColor);
        Assert.Equal(Color.Empty, style.SelectionForeColor);
        Assert.Null(style.Tag);
        Assert.Equal(DataGridViewTriState.NotSet, style.WrapMode);
    }

    public static IEnumerable<object[]> Equals_TestData()
    {
        Font font = SystemFonts.DefaultFont;
        NumberFormatInfo formatProvider = new();

        yield return new object[] { new DataGridViewCellStyle(), new DataGridViewCellStyle(), true };

        yield return new object[]
        {
            new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.BottomCenter },
            new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.BottomCenter },
            true
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.BottomCenter },
            new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.BottomRight },
            false
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { BackColor = Color.Red },
            new DataGridViewCellStyle { BackColor = Color.Red },
            true
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { BackColor = Color.Red },
            new DataGridViewCellStyle { BackColor = Color.Blue },
            false
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { DataSourceNullValue = "dbNull" },
            new DataGridViewCellStyle { DataSourceNullValue = "dbNull" },
            true
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { DataSourceNullValue = "dbNull" },
            new DataGridViewCellStyle { DataSourceNullValue = "other" },
            false
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { DataSourceNullValue = null },
            new DataGridViewCellStyle { DataSourceNullValue = null },
            true
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { DataSourceNullValue = null },
            new DataGridViewCellStyle { DataSourceNullValue = "other" },
            false
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { ForeColor = Color.Red },
            new DataGridViewCellStyle { ForeColor = Color.Red },
            true
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { ForeColor = Color.Red },
            new DataGridViewCellStyle { ForeColor = Color.Blue },
            false
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { Font = font },
            new DataGridViewCellStyle { Font = font },
            true
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { Font = font },
            new DataGridViewCellStyle { Font = SystemFonts.MenuFont },
            false
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { Format = "format" },
            new DataGridViewCellStyle { Format = "format" },
            true
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { Format = "format" },
            new DataGridViewCellStyle { Format = "other" },
            false
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { FormatProvider = formatProvider },
            new DataGridViewCellStyle { FormatProvider = formatProvider },
            true
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { FormatProvider = formatProvider },
            new DataGridViewCellStyle { FormatProvider = CultureInfo.CurrentCulture },
            false, true
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { NullValue = "null" },
            new DataGridViewCellStyle { NullValue = "null" },
            true
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { NullValue = "null" },
            new DataGridViewCellStyle { NullValue = "other" },
            false
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { NullValue = null },
            new DataGridViewCellStyle { NullValue = null },
            true
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { NullValue = null },
            new DataGridViewCellStyle { NullValue = "other" },
            false
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { Padding = new Padding(1, 2, 3, 4) },
            new DataGridViewCellStyle { Padding = new Padding(1, 2, 3, 4) },
            true
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { Padding = new Padding(1, 2, 3, 4) },
            new DataGridViewCellStyle { Padding = new Padding(2, 3, 4, 5) },
            false
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { SelectionBackColor = Color.Red },
            new DataGridViewCellStyle { SelectionBackColor = Color.Red },
            true
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { SelectionBackColor = Color.Red },
            new DataGridViewCellStyle { SelectionBackColor = Color.Blue },
            false
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { SelectionForeColor = Color.Red },
            new DataGridViewCellStyle { SelectionForeColor = Color.Red },
            true
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { SelectionForeColor = Color.Red },
            new DataGridViewCellStyle { SelectionForeColor = Color.Blue },
            false
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { Tag = "tag" },
            new DataGridViewCellStyle { Tag = "tag" },
            true
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { Tag = "tag" },
            new DataGridViewCellStyle { Tag = "other" },
            false
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True },
            new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True },
            true
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True },
            new DataGridViewCellStyle { WrapMode = DataGridViewTriState.False },
            false
        };

        yield return new object[] { new DataGridViewCellStyle(), new(), false };
        yield return new object[] { new DataGridViewCellStyle(), null, false };
    }

    [Theory]
    [MemberData(nameof(Equals_TestData))]
    public void DataGridViewCellStyle_Equals_Invoke_ReturnsExpected(DataGridViewCellStyle style, object other, bool expected, bool? expectedEqualHashCode = null)
    {
        if (other is DataGridViewCellStyle)
        {
            Assert.Equal(expectedEqualHashCode ?? expected, style.GetHashCode().Equals(other.GetHashCode()));
        }

        Assert.Equal(expected, style.Equals(other));
    }

    public static IEnumerable<object[]> ToString_TestData()
    {
        yield return new object[] { new DataGridViewCellStyle(), "DataGridViewCellStyle { }" };
        yield return new object[]
        {
            new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.BottomCenter },
            "DataGridViewCellStyle { Alignment=BottomCenter }"
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { BackColor = Color.Red },
            "DataGridViewCellStyle { BackColor=Color [Red] }"
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { DataSourceNullValue = "dbNull" },
            "DataGridViewCellStyle { DataSourceNullValue=dbNull }"
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { DataSourceNullValue = null },
            "DataGridViewCellStyle { }"
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { ForeColor = Color.Red },
            "DataGridViewCellStyle { ForeColor=Color [Red] }"
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { Font = SystemFonts.DefaultFont },
            $"DataGridViewCellStyle {{ Font={SystemFonts.DefaultFont} }}"
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { Format = "format" },
            "DataGridViewCellStyle { Format=format }"
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { FormatProvider = new NumberFormatInfo() },
            "DataGridViewCellStyle { }"
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { NullValue = "null" },
            "DataGridViewCellStyle { NullValue=null }"
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { NullValue = null },
            "DataGridViewCellStyle { }"
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { Padding = new Padding(1, 2, 3, 4) },
            "DataGridViewCellStyle { Padding={Left=1,Top=2,Right=3,Bottom=4} }"
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { SelectionBackColor = Color.Red },
            "DataGridViewCellStyle { SelectionBackColor=Color [Red] }"
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { SelectionForeColor = Color.Red },
            "DataGridViewCellStyle { SelectionForeColor=Color [Red] }"
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { Tag = "tag" },
            "DataGridViewCellStyle { Tag=tag }"
        };
        yield return new object[]
        {
            new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True },
            "DataGridViewCellStyle { WrapMode=True }"
        };

        Font font = SystemFonts.DefaultFont;
        yield return new object[]
        {
            new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.BottomCenter,
                BackColor = Color.Red,
                DataSourceNullValue = "dbNull",
                Font = font,
                ForeColor = Color.Blue,
                Format = "format",
                FormatProvider = new NumberFormatInfo(),
                NullValue = "null",
                Padding = new Padding(1, 2, 3, 4),
                SelectionBackColor = Color.Green,
                SelectionForeColor = Color.Yellow,
                Tag = "tag",
                WrapMode = DataGridViewTriState.True
            },
            $"DataGridViewCellStyle {{ BackColor=Color [Red], ForeColor=Color [Blue], SelectionBackColor=Color [Green], SelectionForeColor=Color [Yellow], Font={font}, NullValue=null, DataSourceNullValue=dbNull, Format=format, WrapMode=True, Alignment=BottomCenter, Padding={{Left=1,Top=2,Right=3,Bottom=4}}, Tag=tag }}"
        };
    }

    [Theory]
    [MemberData(nameof(ToString_TestData))]
    public void DataGridViewCellStyle_ToString_Invoke_ReturnsExpected(DataGridViewCellStyle style, string expected)
    {
        Assert.Equal(expected, style.ToString());
    }

    private class AlwaysEqual
    {
        public override bool Equals(object other) => other is AlwaysEqual;

        public override int GetHashCode() => base.GetHashCode();
    }
}
