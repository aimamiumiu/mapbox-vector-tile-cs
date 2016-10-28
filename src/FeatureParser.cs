﻿using System.Collections.Generic;
using System.Globalization;

namespace mapbox.vector.tile
{
    public static class FeatureParser
    {

        public static VectorTileFeature ParseNew(Tile.Feature feature, List<string> keys, List<Tile.Value> values,uint extent)
        {
            VectorTileFeature result = new VectorTileFeature(); 
            var id = feature.Id;

            var geom =  GeometryParser.ParseGeometry(feature.Geometry, feature.Type);
            result.GeometryType = feature.Type;

            // add the geometry
            result.Geometry = geom;

            // now add the attributes
            if (result != null)
            {
                result.Id = id.ToString(CultureInfo.InvariantCulture);
                result.Attributes = AttributesParser.Parse(keys, values, feature.Tags);
            }
            return result;
        }
    }
}
