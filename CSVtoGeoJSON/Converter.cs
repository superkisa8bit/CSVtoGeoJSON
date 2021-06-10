using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using System.IO;
using System.Globalization;

namespace CSVtoGeoJSON
{
    public class Converter
    {
        private string latPath = "lon_M01_20210212_04_03_3.csv";
        private string lonPath = "lat_M01_20210212_04_03_3.csv";
        private string gp100Path = "gp_M01_20210212_04_03_3_100.csv";
        private string gp200Path = "gp_M01_20210212_04_03_3_200.csv";
        private string gp300Path = "gp_M01_20210212_04_03_3_300.csv";
        private string gp400Path = "gp_M01_20210212_04_03_3_400.csv";
        private string gp500Path = "gp_M01_20210212_04_03_3_500.csv";
        private string gp700Path = "gp_M01_20210212_04_03_3_700.csv";
        private string gp850Path = "gp_M01_20210212_04_03_3_850.csv";
        private string gp925Path = "gp_M01_20210212_04_03_3_925.csv";
        private string gp1000Path = "gp_M01_20210212_04_03_3_1000.csv";

        private string tmp100Path = "tmp_M01_20210212_04_03_3_100.csv";
        private string tmp200Path = "tmp_M01_20210212_04_03_3_200.csv";
        private string tmp300Path = "tmp_M01_20210212_04_03_3_300.csv";
        private string tmp400Path = "tmp_M01_20210212_04_03_3_400.csv";
        private string tmp500Path = "tmp_M01_20210212_04_03_3_500.csv";
        private string tmp700Path = "tmp_M01_20210212_04_03_3_700.csv";
        private string tmp850Path = "tmp_M01_20210212_04_03_3_850.csv";
        private string tmp925Path = "tmp_M01_20210212_04_03_3_925.csv";
        private string tmp1000Path = "tmp_M01_20210212_04_03_3_1000.csv";

        public Converter() { }

        public async Task<double[]> LoadDataFromFile(FileInfo pathToFile)
        {
            if (!pathToFile.Exists) throw new Exception("File not found");
            var lines = await File.ReadAllLinesAsync(pathToFile.FullName);
            List<double> numbers = new List<double>();
            foreach (var data in lines)
            {
                var lineArray = data.Split(';').Select(c => double.Parse(c, CultureInfo.InvariantCulture));
                numbers.AddRange(lineArray);
            }
            return numbers.ToArray();
        }
        public async Task<List<DataPoint>> GeneratePoints()
        {
            List<DataPoint> points = new List<DataPoint>();

            double[] lat = await LoadDataFromFile(new FileInfo(latPath));
            double[] lon = await LoadDataFromFile(new FileInfo(lonPath));
            double[] gp100 = await LoadDataFromFile(new FileInfo(gp100Path));
            double[] gp200 = await LoadDataFromFile(new FileInfo(gp200Path));
            double[] gp300 = await LoadDataFromFile(new FileInfo(gp300Path));
            double[] gp400 = await LoadDataFromFile(new FileInfo(gp400Path));
            double[] gp500 = await LoadDataFromFile(new FileInfo(gp500Path));
            double[] gp700 = await LoadDataFromFile(new FileInfo(gp700Path));
            double[] gp850 = await LoadDataFromFile(new FileInfo(gp850Path));
            double[] gp925 = await LoadDataFromFile(new FileInfo(gp925Path));
            double[] gp1000 = await LoadDataFromFile(new FileInfo(gp1000Path));
            double[] tmp100 = await LoadDataFromFile(new FileInfo(tmp100Path));
            double[] tmp200 = await LoadDataFromFile(new FileInfo(tmp200Path));
            double[] tmp300 = await LoadDataFromFile(new FileInfo(tmp300Path));
            double[] tmp400 = await LoadDataFromFile(new FileInfo(tmp400Path));
            double[] tmp500 = await LoadDataFromFile(new FileInfo(tmp500Path));
            double[] tmp700 = await LoadDataFromFile(new FileInfo(tmp700Path));
            double[] tmp850 = await LoadDataFromFile(new FileInfo(tmp850Path));
            double[] tmp925 = await LoadDataFromFile(new FileInfo(tmp925Path));
            double[] tmp1000 = await LoadDataFromFile(new FileInfo(tmp1000Path));

            for (int i = 0; i < lat.Length; i++)
            {
                points.Add(new DataPoint()
                {
                    Latitude = Math.Round(lat[i], 6),
                    Longitude = Math.Round(lon[i], 6),
                    Data100 = gp100[i],
                    Data200 = gp200[i],
                    Data300 = gp300[i],
                    Data400 = gp400[i],
                    Data500 = gp500[i],
                    Data700 = gp700[i],
                    Data850 = gp850[i],
                    Data925 = gp925[i],
                    Data1000 = gp1000[i],
                    DataTmp100 = tmp100[i],
                    DataTmp200 = tmp200[i],
                    DataTmp300 = tmp300[i],
                    DataTmp400 = tmp400[i],
                    DataTmp500 = tmp500[i],
                    DataTmp700 = tmp700[i],
                    DataTmp850 = tmp850[i],
                    DataTmp925 = tmp925[i],
                    DataTmp1000 = tmp1000[i]
                });
            }
            return points;
        }

        public async void GetJson()
        {
            List<Feature> features = new List<Feature>();
            foreach (var point in await GeneratePoints())
            {
                Position position = new Position(point.Latitude, point.Longitude);
                Dictionary<string, dynamic> properties = new Dictionary<string, dynamic>
                {
                    { "GP100", Math.Round(point.Data100, 6) },
                    { "GP200", Math.Round(point.Data200, 6) },
                    { "GP300", Math.Round(point.Data300, 6) },
                    { "GP400", Math.Round(point.Data400, 6) },
                    { "GP500", Math.Round(point.Data500, 6) },
                    { "GP700", Math.Round(point.Data700, 6) },
                    { "GP850", Math.Round(point.Data850, 6) },
                    { "GP925", Math.Round(point.Data925, 6) },
                    { "GP1000", Math.Round(point.Data1000, 6) },
                    { "TMP100", Math.Round(point.DataTmp100, 6) },
                    { "TMP200", Math.Round(point.DataTmp200, 6) },
                    { "TMP300", Math.Round(point.DataTmp300, 6) },
                    { "TMP400", Math.Round(point.DataTmp400, 6) },
                    { "TMP500", Math.Round(point.DataTmp500, 6) },
                    { "TMP700", Math.Round(point.DataTmp700, 6) },
                    { "TMP850", Math.Round(point.DataTmp850, 6) },
                    { "TMP925", Math.Round(point.DataTmp925, 6) },
                    { "TMP1000", Math.Round(point.DataTmp1000, 6) },
                };
                Feature feature = new Feature(new Point(position), properties);
                features.Add(feature);

            }

            using (StreamWriter streamWriter = new StreamWriter("points.json", false))
            {
                streamWriter.WriteLine(JsonConvert.SerializeObject(features));
            }
        }

    }
}