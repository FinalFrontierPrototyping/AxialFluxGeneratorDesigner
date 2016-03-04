#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Xml;
using AxialFluxGeneratorDesigner.Calculations;

#endregion

namespace AxialFluxGeneratorDesigner.Gui
{
    /// <summary>
    ///     This class handles the configuration file reading and writing.
    /// </summary>
    public static class FileHandling
    {
        /// <summary>
        ///     This method writes the user input properties to a configuration file.
        /// </summary>
        /// <param name="propertyList"></param>
        /// <param name="fileName"></param>
        public static void Write(List<GeneratorProperty<double>> propertyList, string fileName)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                NewLineOnAttributes = true
            };

            var filePath = fileName;
            Debug.WriteLine(nameof(filePath) + ": " + filePath);

            using (var writer = XmlWriter.Create(filePath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Generator");

                foreach (var s in propertyList)
                {
                    writer.WriteStartElement("GeneratorProperty");

                    writer.WriteElementString("Name", s.Name);
                    writer.WriteElementString("Value", s.Value.ToString(CultureInfo.InvariantCulture));
                    writer.WriteElementString("Min", s.Min.ToString(CultureInfo.InvariantCulture));
                    writer.WriteElementString("Max", s.Max.ToString(CultureInfo.InvariantCulture));

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();

                Debug.WriteLine("Properties written to XML file: " + propertyList.Count);
            }
        }

        /// <summary>
        ///     This method reads the user input properties from a configuration file to the Generator class instance..
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<GeneratorProperty<double>> Read(string path)
        {
            var final = new List<GeneratorProperty<double>>();

            try
            {
                // Create an XML reader for this file.
                using (var reader = XmlReader.Create(path))
                {
                    while (reader.Read())
                    {
                        if (reader.Name == "GeneratorProperty")
                        {
                            var value = 0.0;
                            var min = 0.0;
                            var max = 0.0;

                            while (reader.Read())
                            {
                                if (reader.Name == "Name")
                                {
                                    reader.Read();
                                    var name = reader.Value;

                                    while (reader.Read())
                                    {
                                        if (reader.Name == "Value")
                                        {
                                            reader.Read();
                                            value = double.Parse(reader.Value);
                                            break;
                                        }
                                        if (reader.Name == "Min")
                                        {
                                            reader.Read();
                                            min = double.Parse(reader.Value);
                                            break;
                                        }
                                        if (reader.Name == "Max")
                                        {
                                            reader.Read();
                                            max = double.Parse(reader.Value);
                                            break;
                                        }
                                    }

                                    var temp = new GeneratorProperty<double>(name, value, min, max);
                                    Debug.WriteLine("Property Name: " + temp.Name + " Value: " + temp.Value + " Min: " +
                                                    temp.Min + " Max: " + temp.Max);

                                    final.Add(temp);

                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while reading configuration file: " + ex.Message);
            }

            Debug.WriteLine("Properties read from XML: " + final.Count);
            return final;
        }
    }
}