﻿// Copyright ©  De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Threading.Tasks;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Services.Signing;

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Mapping
{
    public class ZippedSignedContentFormatter
    {
        private const string ContentEntryName = "content.bin";
        private const string SignaturesEntryName = "content.sig";

        private readonly ISigner _Signer;

        public ZippedSignedContentFormatter(ISigner signer)
        {
            _Signer = signer;
        }

        public async Task<byte[]> SignedContentPacket(byte[] content)
        {
            var signature = _Signer.GetSignature(content);
            return await CreateZipArchive(content, signature);
        }

        private static async Task<byte[]> CreateZipArchive(byte[] content, byte[] signature)
        {
            await using var result = new MemoryStream();
            using (var archive = new ZipArchive(result, ZipArchiveMode.Create, true))
            {
                await WriteEntry(archive, ContentEntryName, content);
                await WriteEntry(archive, SignaturesEntryName, signature);
            }

            return result.ToArray();
        }

        private static async Task WriteEntry(ZipArchive archive, string entryName, byte[] content)
        {
            await using var entryStream = archive.CreateEntry(entryName).Open();
            await using var contentStream = new MemoryStream(content);
            await contentStream.CopyToAsync(entryStream);
        }
    }
}