using OSGeo.OGR;
using OSGeo.OSR;
using System;

/// <summary>
/// 使用OGR读取矢量数据信息类
/// </summary>
public class OGRReadFile
{
    public static string GetVectorInfo(string strFilePath)
    {
        string strInfomation = "";

        /* -------------------------------------------------------------------- */
        /*      Register format(s).                                             */
        /* -------------------------------------------------------------------- */
        Ogr.RegisterAll();

        /* -------------------------------------------------------------------- */
        /*      Open data source.                                               */
        /* -------------------------------------------------------------------- */
        DataSource ds = Ogr.Open(strFilePath, 0);
        if (ds == null)
        {
            strInfomation = ("Can't open " + strFilePath);
            return strInfomation;
        }

        /* -------------------------------------------------------------------- */
        /*      Get driver                                                      */
        /* -------------------------------------------------------------------- */
        Driver drv = ds.GetDriver();
        if (drv == null)
        {
            strInfomation = ("Can't get driver.");
            return strInfomation;
        }

        // TODO: drv.name is still unsafe with lazy initialization (Bug 1339)
        strInfomation += ("Using driver " + drv.name);

        /* -------------------------------------------------------------------- */
        /*      Iterating through the layers                                    */
        /* -------------------------------------------------------------------- */

        for (int iLayer = 0; iLayer < ds.GetLayerCount(); iLayer++)
        {
            Layer layer = ds.GetLayerByIndex(iLayer);

            if (layer == null)
            {
                strInfomation = ("FAILURE: Couldn't fetch advertised layer " + iLayer);
                return strInfomation;
            }

            strInfomation += ReportLayer(layer);
        }

        return strInfomation;
    }

    public static string ReportLayer(Layer layer)
    {
        string strInfomation = "";
        FeatureDefn def = layer.GetLayerDefn();
        strInfomation += ("Layer name: " + def.GetName());
        strInfomation += ("Feature Count: " + layer.GetFeatureCount(1).ToString());
        Envelope ext = new Envelope();
        layer.GetExtent(ext, 1);
        strInfomation += ("Extent: " + ext.MinX.ToString() + "," + ext.MaxX.ToString() + "," +
            ext.MinY.ToString() + "," + ext.MaxY.ToString());

        /* -------------------------------------------------------------------- */
        /*      Reading the spatial reference                                   */
        /* -------------------------------------------------------------------- */
        OSGeo.OSR.SpatialReference sr = layer.GetSpatialRef();
        string srs_wkt;
        if (sr != null)
        {
            sr.ExportToPrettyWkt(out srs_wkt, 1);
        }
        else
            srs_wkt = "(unknown)";


        strInfomation += ("Layer SRS WKT: " + srs_wkt);

        /* -------------------------------------------------------------------- */
        /*      Reading the fields                                              */
        /* -------------------------------------------------------------------- */
        strInfomation += ("Field definition:");
        for (int iAttr = 0; iAttr < def.GetFieldCount(); iAttr++)
        {
            FieldDefn fdef = def.GetFieldDefn(iAttr);

            strInfomation += (fdef.GetNameRef() + ": " +
                 fdef.GetFieldTypeName(fdef.GetFieldType()) + " (" +
                 fdef.GetWidth().ToString() + "." +
                 fdef.GetPrecision().ToString() + ")");
        }

        /* -------------------------------------------------------------------- */
        /*      Reading the shapes                                              */
        /* -------------------------------------------------------------------- */
        strInfomation += ("");
        Feature feat;
        while ((feat = layer.GetNextFeature()) != null)
        {
            strInfomation += ReportFeature(feat, def);
            feat.Dispose();
        }

        return strInfomation;
    }

    public static string ReportFeature(Feature feat, FeatureDefn def)
    {
        string strInfomation = "";
        strInfomation += ("Feature(" + def.GetName() + "): " + feat.GetFID().ToString());
        for (int iField = 0; iField < feat.GetFieldCount(); iField++)
        {
            FieldDefn fdef = def.GetFieldDefn(iField);

            strInfomation += (fdef.GetNameRef() + " (" +
                fdef.GetFieldTypeName(fdef.GetFieldType()) + ") = ");

            if (feat.IsFieldSet(iField))
            {
                if (fdef.GetFieldType() == FieldType.OFTStringList)
                {
                    string[] sList = feat.GetFieldAsStringList(iField);
                    foreach (string s in sList)
                    {
                        strInfomation += ("\"" + s + "\" ");
                    }
                    strInfomation += ("\n");
                }
                else if (fdef.GetFieldType() == FieldType.OFTIntegerList)
                {
                    int count;
                    int[] iList = feat.GetFieldAsIntegerList(iField, out count);
                    for (int i = 0; i < count; i++)
                    {
                        strInfomation += (iList[i] + " ");
                    }
                    strInfomation += ("\n");
                }
                else if (fdef.GetFieldType() == FieldType.OFTRealList)
                {
                    int count;
                    double[] iList = feat.GetFieldAsDoubleList(iField, out count);
                    for (int i = 0; i < count; i++)
                    {
                        strInfomation += (iList[i].ToString() + " ");
                    }
                    strInfomation += ("\n");
                }
                else
                    strInfomation += (feat.GetFieldAsString(iField));
            }
            else
                strInfomation += ("(null)");

        }

        if (feat.GetStyleString() != null)
            strInfomation += ("  Style = " + feat.GetStyleString());

        Geometry geom = feat.GetGeometryRef();
        if (geom != null)
        {
            strInfomation += ("  " + geom.GetGeometryName() +
                "(" + geom.GetGeometryType() + ")");
            Geometry sub_geom;
            for (int i = 0; i < geom.GetGeometryCount(); i++)
            {
                sub_geom = geom.GetGeometryRef(i);
                if (sub_geom != null)
                {
                    strInfomation += ("  subgeom" + i + ": " + sub_geom.GetGeometryName() +
                        "(" + sub_geom.GetGeometryType() + ")");
                }
            }
            Envelope env = new Envelope();
            geom.GetEnvelope(env);
            strInfomation += ("   ENVELOPE: " + env.MinX + "," + env.MaxX + "," +
                env.MinY + "," + env.MaxY);

            string geom_wkt;
            geom.ExportToWkt(out geom_wkt);
            strInfomation += ("  " + geom_wkt);
        }

        strInfomation += ("\n");
        return strInfomation;
    }
}
