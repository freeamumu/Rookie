using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAL_O
{
    public class Get
    {
        private Layer oLayer;
        public string sCoordiantes;
        public bool GetGeometry(int iIndex)
        {
            if (null == oLayer)
            {
                return false;
            }
            int iFeatureCout = Convert.ToInt32(oLayer.GetFeatureCount(0)) ;
            Feature oFeature = null;
            oFeature = oLayer.GetFeature(iIndex);
            //  Geometry
            Geometry oGeometry = oFeature.GetGeometryRef();
            wkbGeometryType oGeometryType = oGeometry.GetGeometryType();
            switch (oGeometryType)
            {
                case wkbGeometryType.wkbPoint:
                    oGeometry.ExportToWkt(out sCoordiantes);
                    sCoordiantes = sCoordiantes.ToUpper().Replace("POINT (", "").Replace(")", "");
                    break;
                case wkbGeometryType.wkbLineString:
                case wkbGeometryType.wkbLinearRing:
                    oGeometry.ExportToWkt(out sCoordiantes);
                    sCoordiantes = sCoordiantes.ToUpper().Replace("LINESTRING (", "").Replace(")", "");
                    break;
                default:
                    break;
            }
            return false;
        }

    }
}
