﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Pulse.Core;
using Pulse.FS;
using Pulse.UI;

namespace Pulse
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //using (FileStream file = File.OpenRead(@"D:\Temp\FFXIII\1\gui\resident\system.grs"))
            //{
            //    WpdHeader header = WpdHeader.ReadFromStream(file);
            //    for (int index = 0; index < header.Extensions.Length; index++)
            //    {
            //        using (FileStream output = File.Create(@"D:\Temp\FFXIII\system.grs\" + header.Names[index] + '.' + header.Extensions[index]))
            //                new StreamSegment(file, header.Offsets[index], header.Lengths[index]).CopyTo(output);
            //    }
            //}
            //Environment.Exit(0);
            //foreach (var file in Directory.GetFiles(@"D:\Temp\FFXIII\txtres\resident\book", "*us*.ztr", SearchOption.AllDirectories))
            //{
            //    using (var input = File.OpenRead(file))
            //    using (var output = File.Create(file + ".txt"))
            //    {
            //        ZtrFileUnpacker unpacker = new ZtrFileUnpacker(input, InteractionService.TextEncoding.Provide()["us"]);
            //        ZtrFileEntry[] entries = unpacker.Unpack();
            //
            //        ZtrTextWriter writer = new ZtrTextWriter(output);
            //        writer.Write(file, entries);
            //    }
            //}
            //Environment.Exit(0);
            //new MainWindow().Show();

            //const string targetListing = @"D:\Steam\SteamApps\common\FINAL FANTASY XIII\white_data\sys\filelistc.win32.bin";
            //const string targetBinary = @"D:\Steam\SteamApps\common\FINAL FANTASY XIII\white_data\sys\white_imgc.win32.bin";
            //
            //ArchiveAccessor sourceAccessor = new ArchiveAccessor(targetBinary, targetListing);
            //ArchiveListing listing = ArchiveListingReader.Read(@"D:\Steam\SteamApps\common\FINAL FANTASY XIII", sourceAccessor).First(l => l.Name == "filelistc.win32.bin");
            
            //using (FileStream input = File.OpenRead(@"D:\Temp\FFXIII\txtres\resident\system\txtres_us.ztr.txt"))
            //using (FileStream output = File.Create(@"D:\Temp\FFXIII\txtres\resident\system\txtres_us.ztr.new"))
            //{
            //    string name;
            //    ZtrTextReader reader = new ZtrTextReader(input);
            //    ZtrFileEntry[] entries = reader.Read(out name);
            //
            //    ZtrFilePacker packer = new ZtrFilePacker(output);
            //    packer.Pack(entries);
            //}
            
            //ArchiveListingEntry entry1 = listing.First(n => n.Name.EndsWith(@"gui/resident/system.win32.xgr"));
            //
            //int compressedSize1;
            //using (Stream input = File.OpenRead(@"D:\Temp\FFXIII\1\gui\resident\system.win32.xgr"))
            //    //using (Stream buff = ZLibHelper.ReplaceEntryContent(input, entry1, out compressedSize1))
            //using (Stream output = sourceAccessor.OpenOrAppendBinary(entry1, (int)input.Length))
            //{
            //    entry1.Size = entry1.UncompressedSize = input.Length;
            //
            //    byte[] copyBuff = new byte[4096];
            //    input.CopyTo(output, (int)input.Length, copyBuff);
            //}
            //
            //ArchiveListingWriter.Write(listing, sourceAccessor);

            new UiMainWindow().Show();
        }
    }
}