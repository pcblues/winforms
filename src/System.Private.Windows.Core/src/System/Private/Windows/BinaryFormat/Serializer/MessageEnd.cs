﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Private.Windows.BinaryFormat.Serializer;

/// <summary>
///  Record that marks the end of the binary format stream.
/// </summary>
internal sealed class MessageEnd : IRecord<MessageEnd>
{
    public static MessageEnd Instance { get; } = new();

    private MessageEnd() { }

    public static RecordType RecordType => RecordType.MessageEnd;

    internal static void Write(BinaryWriter writer) => writer.Write((byte)RecordType);
}
