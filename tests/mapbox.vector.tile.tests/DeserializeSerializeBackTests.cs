﻿using Mapbox.Vector.Tile;
using NUnit.Framework;
using ProtoBuf;
using System.IO;
using System.Reflection;

namespace mapbox.vector.tile.tests
{
    public class DeserializeSerializeBackTests
    {
        [Test]
        public void TestIssue16()
        {
            string pbf = "mapbox.vector.tile.tests.testdata.16_34440_23455_raw.mvt";
            var pbfStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(pbf);

            // deserialize the tile first
            var tile = Serializer.Deserialize<Tile>(pbfStream);
            Assert.IsTrue(tile.Layers[4].Name == "road___1");
            Assert.IsTrue(tile.Layers[4].Values[1].IntValue == 0);
            Assert.IsTrue(tile.Layers[4].Values[1].HasIntValue);
            Assert.IsTrue(tile.Layers[4].Values[0].StringValue == "");
            Assert.IsTrue(tile.Layers[4].Values[0].HasStringValue);

            // it is enough to serialize into stream
            var serializedTileStream = new MemoryStream();
            Serializer.Serialize(serializedTileStream, tile);

            // read the stream again
            serializedTileStream.Seek(0, SeekOrigin.Begin);
            var deserializedTile = Serializer.Deserialize<Tile>(serializedTileStream);

            Assert.IsTrue(deserializedTile.Layers[4].Name == "road___1");
            Assert.IsTrue(deserializedTile.Layers[4].Values[1].IntValue == 0);
            Assert.IsTrue(deserializedTile.Layers[4].Values[1].HasIntValue);
            Assert.IsTrue(deserializedTile.Layers[4].Values[0].StringValue == "");
            Assert.IsTrue(deserializedTile.Layers[4].Values[0].HasStringValue);
            Assert.IsFalse(deserializedTile.Layers[4].Values[1].HasFloatValue);
            Assert.IsFalse(deserializedTile.Layers[4].Values[1].HasStringValue);
        }
    }
}
