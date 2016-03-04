#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using AxialFluxGeneratorDesigner.Calculations;

#endregion

namespace AxialFluxGeneratorDesigner.Gui
{
    /// <summary>
    ///     This is the GUI part of the AFPMG Designer
    /// </summary>
    public partial class FormAfpmDesigner : Form
    {
        /// <summary>
        ///     The Class containing the code to perform calculations
        /// </summary>
        private readonly Generator _generator = new Generator();

        /// <summary>
        ///     Check if the form GUI components are initialized.
        /// </summary>
        private bool _isInitialized;

        /// <summary>
        ///     The constructor of the Form class.
        /// </summary>
        public FormAfpmDesigner()
        {
            InitializeComponent();
            tabGenerator.SelectedIndexChanged += Tabs_SelectedIndexChanged;
        }

        /// <summary>
        ///     This method is called when the form is loaded
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            //Disables processing of numericUpDown control events 
            _isInitialized = false;

            //Hide tab pages
            tabGenerator.TabPages.Remove(tabIterator);
            tabGenerator.TabPages.Remove(tabCharts);
            tabGenerator.TabPages.Remove(tab3D);

            //Fill Energy storage combo-box with data
            cmbEnergyStorage.Items.Add("Battery connection");
            cmbEnergyStorage.Items.Add("Grid connection");

            //Fill Front end selection combo-box with data
            cmbGeneratorFrontEnd.Items.Add("Wind turbine");
            cmbGeneratorFrontEnd.Items.Add("Other");

            //Set magnet properties
            foreach (var element in _generator.MagnetProperties)
            {
                cmbMagnetGrade.Items.Add(element.Item2);
            }

            //Read and process last saved setting file
            var path = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(path);
            var generatePropertyList = FileHandling.Read(directory + "/PropertyList.cfg");
            ProcessPropertyList(generatePropertyList);

            //Enables processing of numericUpDown control events 

            //Update User input controls (combo-boxes and numeric up/down controls so all correct restored settings are showed.
            UpdateUserInputControls();
            UpdateMagnetProperties();
            UpdateComboBoxes();

            _isInitialized = true;

            // Initially update calculations and update GUI
            _generator.UpdateCalculations(false);
            UpdateGuiElements();
        }

        /// <summary>
        ///     This method acts as a wrapper to easy assign GeneratorProperty values to a NumericUpDown control
        /// </summary>
        /// <param name="num"></param>
        /// <param name="prop"></param>
        private static void SetUserInputControl(NumericUpDown num, GeneratorProperty<double> prop)
        {
            num.Minimum = (decimal) prop.Min;
            num.Maximum = (decimal) prop.Max;
            num.Value = (decimal) prop.Value;
        }

        /// <summary>
        ///     This method updates all the user input controls (e.g. numericUpDown and combo-box controls).
        /// </summary>
        private void UpdateUserInputControls()
        {
            cmbEnergyStorage.SelectedIndex = (int) _generator.GeneratorEnergyStorageConnection.Value;
            cmbGeneratorFrontEnd.SelectedIndex = (int) _generator.GeneratorFrontEnd.Value;
            cmbMagnetGrade.SelectedIndex = (int) _generator.MagnetGrade.Value;

            //Set min, max and default values of numeric up/down controls

            //Battery storage
            if (cmbEnergyStorage.SelectedIndex == 0)
            {
                SetUserInputControl(numEnergyStorageVoltageMin, _generator.DcBatteryVoltage);
            }
            //Inverter to grid storage
            else
            {
                SetUserInputControl(numEnergyStorageVoltageMin, _generator.DcInverterVoltageMin);
                SetUserInputControl(numEnergyStorageVoltageMax, _generator.DcInverterVoltageMax);
            }
            
            SetUserInputControl(numEnergyStoragePower, _generator.GeneratorPower);

            //Front end
            SetUserInputControl(numWindSpeedNom, _generator.TurbineWindspeedMax);
            SetUserInputControl(numTipRatioNom, _generator.TurbineSpeedTipRatioMax);
            SetUserInputControl(numRpmMax, _generator.FrontEndRpmMax);
            SetUserInputControl(numTipRatioCutIn, _generator.TurbineSpeedTipRatioMin);
            SetUserInputControl(numWindSpeedCutIn, _generator.TurbineWindspeedMin);
            SetUserInputControl(numRpmMin, _generator.FrontEndRpmMin);
            SetUserInputControl(numMaximumPowerCoefficient, _generator.TurbineMaximumPowerCoefficient);
            SetUserInputControl(numAirDensity, _generator.TurbineAirDensity);

            //Generator
            SetUserInputControl(numMechanicalGap, _generator.MechamicalGap);
            SetUserInputControl(numGeneratorEfficiency, _generator.GeneratorEfficiency);

            //Stator
            SetUserInputControl(numCoilsPerPhaseCount, _generator.CoilsPerPhase);

            //Coil
            SetUserInputControl(numCoilFillFactor, _generator.CoilFillFactor);
            SetUserInputControl(numMaxCoilCurrentDensity, _generator.MaxCurrentDensity);

            //Coil dimensions
            SetUserInputControl(numBetweenCoilDistance, _generator.BetweenCoilDistance);

            //Rotor
            SetUserInputControl(numPoleArcToPolePitchRatio, _generator.MagnetPoleArcToPolePitchRatio);
            SetUserInputControl(numMagnetThickness, _generator.MagnetThickness);
            SetUserInputControl(numMagnetHeight, _generator.MagnetHeight);
            SetUserInputControl(numMagnetWidth, _generator.MagnetWidth);

            //Rectifier
            SetUserInputControl(numPhaseWireLength, _generator.PhaseWireLength);
            SetUserInputControl(numPhaseWireDiameter, _generator.PhaseWireDiameter);
            SetUserInputControl(numRectifierWireLength, _generator.RectifierWireLength);
            SetUserInputControl(numRectifierWireDiameter, _generator.RectifierWireDiameter);
        }

        /// <summary>
        ///     This method updates the calculations and all GUI controls when triggered (e.g. numericUpDown event handler)
        /// </summary>
        private void UpdateGenerator()
        {
            _generator.UpdateCalculations(false);
            UpdateGuiElements();
        }

        /// <summary>
        ///     This method rounds a value and outputs it as string.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        private static string LabelTextFormat(double prop, int decimals)
        {
            return Math.Round(prop, decimals).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     This method updates all GUI elements
        /// </summary>
        private void UpdateGuiElements()
        {
            //Front end GUI
            numWindSpeedCutIn.Value = (decimal) Math.Round(_generator.TurbineWindspeedMin.Value, 2);
            numRpmMin.Value = (decimal) _generator.FrontEndRpmMin.Value;
            numRpmMax.Value = (int) _generator.FrontEndRpmMax.Value;

            lblTurbineRotorRadiusValue.Text = LabelTextFormat(_generator.TurbineRotorRadius, 2);
            lblTurbineMaximalTorqueValue.Text = LabelTextFormat(_generator.FrontEndTorque, 2);

            //Generator requirements
            lblPhaseVoltageMinValue.Text = LabelTextFormat(_generator.PhaseVoltageMin, 2);
            lblPhaseVoltageMaxValue.Text = LabelTextFormat(_generator.PhaseVoltageMax, 2);
            lblMaxPhaseCurrentValue.Text = LabelTextFormat(_generator.MaxPhaseCurrent, 2);

            //Stator
            lblCoilCountValue.Text = _generator.CoilCount.ToString(CultureInfo.InvariantCulture);
            lblStatorInnerRadiusValue.Text =
                Math.Round(_generator.StatorInnerRadius, 2).ToString(CultureInfo.InvariantCulture);
            lblStatorOuterRadiusValue.Text =
                Math.Round(_generator.StatorOuterRadius, 2).ToString(CultureInfo.InvariantCulture);

            //Stator coil
            lblCoilHeatCoefficientValue.Text =
                Math.Round(_generator.CoilHeatCoefficient, 2).ToString(CultureInfo.InvariantCulture);
            lblCoilTurnsValue.Text = _generator.CoilTurns.ToString();
            lblCoilWireDiameterValue.Text =
                Math.Round(_generator.CoilWireDiameter, 2).ToString(CultureInfo.InvariantCulture);
            lblCoilWireLengthValue.Text = Math.Round(_generator.CoilWireLength, 2)
                .ToString(CultureInfo.InvariantCulture);
            lblCoilResistanceValue.Text = Math.Round(_generator.CoilResistance, 2)
                .ToString(CultureInfo.InvariantCulture);

            //Coil dimensions
            lblCoilAngle.Text = Math.Round(_generator.CoilAngle, 2).ToString(CultureInfo.InvariantCulture);
            lblStatorThicknessValue.Text = Math.Round(_generator.CoilThickness, 2)
                .ToString(CultureInfo.InvariantCulture);
            lblCoilLegWidthValue.Text = Math.Round(_generator.CoilLegWidth, 2).ToString(CultureInfo.InvariantCulture);
            lblAverageCoilTurnLengthValue.Text =
                Math.Round(_generator.CoilAverageTurnLength, 2).ToString(CultureInfo.InvariantCulture);
            lblCoilSideSurfaceValue.Text =
                Math.Round(_generator.CoilSideSurface, 2).ToString(CultureInfo.InvariantCulture);


            lblCoilOuterTopValue.Text = Math.Round(_generator.CoilOuterTop, 2).ToString(CultureInfo.InvariantCulture);
            lblCoilOuterBottomValue.Text =
                Math.Round(_generator.CoilOuterBottom, 2).ToString(CultureInfo.InvariantCulture);
            lblCoilOuterSideValue.Text = Math.Round(_generator.CoilOuterSide, 2).ToString(CultureInfo.InvariantCulture);
            lblInnerCoilTopLengthValue.Text =
                Math.Round(_generator.CoilInnerTop, 2).ToString(CultureInfo.InvariantCulture);
            lblInnerCoilBottomLengthValue.Text =
                Math.Round(_generator.CoilInnerBottom, 2).ToString(CultureInfo.InvariantCulture);
            lblInnerCoilSideLengthValue.Text =
                Math.Round(_generator.CoilInnerSide, 2).ToString(CultureInfo.InvariantCulture);

            //Rotor
            lblRotorThicknessValue.Text = Math.Round(_generator.RotorThickness, 2)
                .ToString(CultureInfo.InvariantCulture);
            lblRotorOuterRadiusValue.Text =
                Math.Round(_generator.RotorOuterRadius, 2).ToString(CultureInfo.InvariantCulture);
            lblRotorInnerRadiusValue.Text =
                Math.Round(_generator.RotorInnerRadius, 2).ToString(CultureInfo.InvariantCulture);
            lblRotorInnerouterRadiusRatioValue.Text =
                Math.Round(_generator.RotorInnerOuterRadiusRatio, 2).ToString(CultureInfo.InvariantCulture);
            lblMagnetCountValue.Text = _generator.MagnetCount.ToString();
            lblMagnetDistanceValue.Text = Math.Round(_generator.MagnetBetweenDistance, 2)
                .ToString(CultureInfo.InvariantCulture);

            //Magnets
            lblMagnetFluxDensityValue.Text =
                Math.Round(_generator.MagnetFluxDensity, 2).ToString(CultureInfo.InvariantCulture);
            lblMagnetFluxValue.Text =
                Math.Round(_generator.MagnetPoleFlux*1000, 2).ToString(CultureInfo.InvariantCulture);

            //Rectifier
            lblPhaseWireVoltageDropValue.Text =
                Math.Round(_generator.PhaseWireVoltageDrop, 2).ToString(CultureInfo.InvariantCulture);
            lblPhaseWireResistanceValue.Text =
                Math.Round(_generator.PhaseWireResistance, 2).ToString(CultureInfo.InvariantCulture);
            lblRectifierWireVoltageDropValue.Text =
                Math.Round(_generator.RectifierWireVoltageDrop, 2).ToString(CultureInfo.InvariantCulture);
            lblRectifierWireResistanceValue.Text =
                Math.Round(_generator.RectifierWireResistance, 2).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     This is the method that is linked to the combo-box cmbMagnetGrade selectedIndexChanged event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbMagnetGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMagnetProperties();
            UpdateGenerator();
        }

        /// <summary>
        ///     This method updates the magnet properties when the cmbMagnetGrade SelectedIndexChanged is triggered.
        /// </summary>
        private void UpdateMagnetProperties()
        {
            _generator.MagnetGrade.Value = _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item1;
            _generator.MagnetRemanentFluxDensity = _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item3;
            _generator.MagnetCoerciveFieldStrength = _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item4;

            lblMagnetRemanenceValue.Text =
                _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item3.ToString(CultureInfo.InvariantCulture);
            lblCoerciveFieldStrengthValue.Text =
                _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item4.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     The method that is triggered if the index of the selected tab is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Generator design tab
            if (tabGenerator.SelectedIndex == 0)
            {
            }
            //Iterator steps tab
            else if (tabGenerator.SelectedIndex == 1)
            {
            }
            //Chart tab
            else if (tabGenerator.SelectedIndex == 2)
            {
            }
            //3D model tab
            else if (tabGenerator.SelectedIndex == 3)
            {
            }
        }

        /// <summary>
        ///     This method is triggered when the load from file button on the menu bar is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var generatePropertyList = new List<GeneratorProperty<double>>();

            var path = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(path);
            openFileDialog1.InitialDirectory = directory;

            openFileDialog1.Filter = @"CFG Files (*.cfg)|*.cfg";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.DefaultExt = @"xml";

            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var file = openFileDialog1.FileName;
                try
                {
                    generatePropertyList = FileHandling.Read(file);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            ProcessPropertyList(generatePropertyList);
        }


        /// <summary>
        ///     This method processes a property file (created using FileHandling.Read()) and sets the value to the correct
        ///     generator property.
        /// </summary>
        /// <param name="generatePropertyList"></param>
        private void ProcessPropertyList(List<GeneratorProperty<double>> generatePropertyList)
        {
            var counterValid = 0;
            var counterTotal = 0;
            var counterInvalid = 0;

            foreach (var property in generatePropertyList)
            {
                try
                {
                    var test = _generator.GetType().GetField(property.Name).GetValue(_generator);

                    if (test != null)
                    {
                        test.GetType().GetProperty("Value").SetValue(test, property.Value, null);
                        counterValid += 1;
                    }
                    else
                    {
                        MessageBox.Show(@"Property not found: " + property.Name);
                        counterInvalid += 1;
                    }

                    counterTotal += 1;
                }
                catch (Exception e)
                {
                    MessageBox.Show(@"Error while reading configuration file. Error: " + e + @". Error in Property: " + property.Name);
                }
            }

            Debug.WriteLine("Total read properties: " + counterTotal);
            Debug.WriteLine("Total matched properties: " + counterValid);
            Debug.WriteLine("Total not matched properties: " + counterInvalid);
        }

        /// <summary>
        ///     This method is triggered when the Save to file button on the menu bar is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(path);
            saveFileDialog1.InitialDirectory = directory;

            saveFileDialog1.Filter = @"CFG Files (*.cfg)|*.cfg";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.DefaultExt = @"cfg";

            var result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var file = saveFileDialog1.FileName;
                try
                {
                    FileHandling.Write(GeneratePropertyList(), file);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        ///     This method is triggered on form close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAfpmDesigner_FormClosing(object sender, FormClosingEventArgs e)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(path);
            FileHandling.Write(GeneratePropertyList(), directory + "/PropertyList.cfg");
        }

        /// <summary>
        ///     This method generates a property file containing all user input control values.
        ///     This file is used when the program is closed and the current values are saved to the configuration file or when the
        ///     user saves a configuration to file.
        /// </summary>
        /// <returns></returns>
        private List<GeneratorProperty<double>> GeneratePropertyList()
        {
            var propertyList = new List<GeneratorProperty<double>>
            {
                _generator.GeneratorEnergyStorageConnection,
                _generator.DcInverterVoltageMin,
                _generator.DcInverterVoltageMax,
                _generator.DcBatteryVoltage,
                _generator.GeneratorPower,
                _generator.GeneratorFrontEnd,
                _generator.TurbineWindspeedMax,
                _generator.TurbineSpeedTipRatioMax,
                _generator.FrontEndRpmMax,
                _generator.TurbineWindspeedMin,
                _generator.TurbineSpeedTipRatioMin,
                _generator.FrontEndRpmMin,
                _generator.TurbineMaximumPowerCoefficient,
                _generator.TurbineAirDensity,
                _generator.MechamicalGap,
                _generator.GeneratorEfficiency,
                _generator.CoilsPerPhase,
                _generator.CoilFillFactor,
                _generator.MaxCurrentDensity,
                _generator.BetweenCoilDistance,
                _generator.MagnetPoleArcToPolePitchRatio,
                _generator.MagnetThickness,
                _generator.MagnetWidth,
                _generator.MagnetHeight,
                _generator.MagnetGrade,
                _generator.PhaseWireLength,
                _generator.PhaseWireDiameter,
                _generator.RectifierWireLength,
                _generator.RectifierWireLength,
                _generator.RectifierWireDiameter
            };
            return propertyList;
        }

        #region Numeric up down events

        private void numInverterVoltageMax_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.DcInverterVoltageMax.Value = (int) numEnergyStorageVoltageMax.Value;
            UpdateGenerator();
        }

        private void numCoilsPerPhaseCount_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.CoilsPerPhase.Value = (int) numCoilsPerPhaseCount.Value;
            UpdateGenerator();
        }

        private void numlMagnetThickness_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.MagnetThickness.Value = (double) numMagnetThickness.Value;
            UpdateGenerator();
        }

        private void numMagnetWidth_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.MagnetWidth.Value = (int) numMagnetWidth.Value;
            UpdateGenerator();
        }

        private void numMagnetLength_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.MagnetHeight.Value = (int) numMagnetHeight.Value;
            UpdateGenerator();
        }

        private void numMechanicalGap_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.MechamicalGap.Value = (double) numMechanicalGap.Value;
            UpdateGenerator();
        }

        private void numBetweenCoilDistance_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.BetweenCoilDistance.Value = (int) numBetweenCoilDistance.Value;
            UpdateGenerator();
        }

        private void numShaftPower_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.GeneratorPower.Value = (int) numEnergyStoragePower.Value;
            UpdateGenerator();
        }

        private void numWindSpeedNom_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.TurbineWindspeedMax.Value = (double) numWindSpeedNom.Value;
            UpdateGenerator();
        }

        private void numTipRatioNom_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.TurbineSpeedTipRatioMax.Value = (double) numTipRatioNom.Value;
            UpdateGenerator();
        }

        private void numInverterVoltageMin_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            if (cmbEnergyStorage.SelectedIndex == 0)
            {
                _generator.DcBatteryVoltage.Value = (int)numEnergyStorageVoltageMin.Value;
            }
            else
            {
                _generator.DcInverterVoltageMin.Value = (int)numEnergyStorageVoltageMin.Value;
            }
            UpdateGenerator();
        }

        private void numTipRatioCutIn_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.TurbineSpeedTipRatioMin.Value = (double) numTipRatioCutIn.Value;
            UpdateGenerator();
        }

        private void numMaximumPowerCoefficient_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.TurbineMaximumPowerCoefficient.Value = (double) numMaximumPowerCoefficient.Value;
            UpdateGenerator();
        }

        private void numAirDensity_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.TurbineAirDensity.Value = (double) numAirDensity.Value;
            UpdateGenerator();
        }

        private void numWindSpeedCutIn_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.TurbineWindspeedMin.Value = (double) numWindSpeedCutIn.Value;
            UpdateGenerator();
        }

        private void numCoilFillFactor_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.CoilFillFactor.Value = (double) numCoilFillFactor.Value;
            UpdateGenerator();
        }

        private void numCoilHeatCoefficient_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.MaxCurrentDensity.Value = (double) numMaxCoilCurrentDensity.Value;
            UpdateGenerator();
        }

        private void numGeneratorEfficiency_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.GeneratorEfficiency.Value = (double) numGeneratorEfficiency.Value;
            UpdateGenerator();
        }

        private void numPhaseWireLength_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.PhaseWireLength.Value = (int) numPhaseWireLength.Value;
            UpdateGenerator();
        }

        private void numPhaseWireDiameter_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.PhaseWireDiameter.Value = (double) numPhaseWireDiameter.Value;
            UpdateGenerator();
        }

        private void numRectifierWireLength_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.RectifierWireLength.Value = (int) numRectifierWireLength.Value;
            UpdateGenerator();
        }

        private void numRectifierWireDiameter_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.RectifierWireDiameter.Value = (double) numRectifierWireDiameter.Value;
            UpdateGenerator();
        }

        private void numRpmMax_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.FrontEndRpmMax.Value = (int) numRpmMax.Value;
            UpdateGenerator();
        }

        private void numRpmMin_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.FrontEndRpmMin.Value = (int) numRpmMin.Value;
            UpdateGenerator();
        }

        private void numPoleArcToPolePitchRatio_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.MagnetPoleArcToPolePitchRatio.Value = (double) numPoleArcToPolePitchRatio.Value;
            UpdateGenerator();
        }

        #endregion

        #region Front end combo box handling

        private void cmbEnergyStorage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            UpdateComboBoxes();
            UpdateGenerator();
        }

        private void cmbGeneratorFrontEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            UpdateComboBoxes();
            UpdateGenerator();
        }

        /// <summary>
        ///     This method updates the front end combo-boxes and numericUpDown controls based on the selected front end.
        /// </summary>
        private void UpdateComboBoxes()
        {
            //Battery
            if (cmbEnergyStorage.SelectedIndex == 0)
            {
                _generator.GeneratorEnergyStorageConnection.Value = 0;
                lblInverterVoltageMin.Text = @"Battery voltage (V)";
                numEnergyStorageVoltageMax.Visible = false;
                numEnergyStorageVoltageMin.Value = (decimal)_generator.DcBatteryVoltage.Value;

                //Wind turbine + Battery
                if (cmbGeneratorFrontEnd.SelectedIndex == 0)
                {
                    _generator.GeneratorFrontEnd.Value = 0;
                    lblFrontEnd.Text = @"Wind turbine front end";

                    numWindSpeedNom.Enabled = true;
                    numTipRatioNom.Enabled = true;
                    numWindSpeedCutIn.Enabled = true;
                    numTipRatioCutIn.Enabled = true;
                    numMaximumPowerCoefficient.Enabled = true;
                    numAirDensity.Enabled = true;
                    lblTurbineRotorRadiusValue.Enabled = true;
                    numRpmMax.Enabled = false;
                    numRpmMin.Enabled = false;
                }
                //Other + Battery
                else if (cmbGeneratorFrontEnd.SelectedIndex == 1)
                {
                    _generator.GeneratorFrontEnd.Value = 1;
                    lblFrontEnd.Text = @"Other front end";

                    numWindSpeedNom.Enabled = false;
                    numTipRatioNom.Enabled = false;
                    numWindSpeedCutIn.Enabled = false;
                    numTipRatioCutIn.Enabled = false;
                    numMaximumPowerCoefficient.Enabled = false;
                    numAirDensity.Enabled = false;
                    lblTurbineRotorRadiusValue.Enabled = false;
                    numRpmMax.Enabled = true;
                    numRpmMin.Enabled = true;
                }
            }
            //Grid
            else if (cmbEnergyStorage.SelectedIndex == 1)
            {
                _generator.GeneratorEnergyStorageConnection.Value = 1;
                lblInverterVoltageMin.Text = @"Minimal voltage (V)";
                numEnergyStorageVoltageMax.Visible = true;
                numEnergyStorageVoltageMin.Value = (decimal) _generator.DcInverterVoltageMin.Value;
                numEnergyStorageVoltageMax.Value = (decimal)_generator.DcInverterVoltageMax.Value;

                //Wind turbine + Grid
                if (cmbGeneratorFrontEnd.SelectedIndex == 0)
                {
                    _generator.GeneratorFrontEnd.Value = 0;
                    lblFrontEnd.Text = @"Wind turbine front end";

                    numWindSpeedNom.Enabled = true;
                    numTipRatioNom.Enabled = true;
                    numWindSpeedCutIn.Enabled = false;
                    numTipRatioCutIn.Enabled = true;
                    numMaximumPowerCoefficient.Enabled = true;
                    numAirDensity.Enabled = true;
                    lblTurbineRotorRadiusValue.Enabled = true;
                    numRpmMax.Enabled = false;
                    numRpmMin.Enabled = false;
                }
                //Other + Grid
                else if (cmbGeneratorFrontEnd.SelectedIndex == 1)
                {
                    _generator.GeneratorFrontEnd.Value = 1;
                    lblFrontEnd.Text = @"Other front end";

                    numWindSpeedNom.Enabled = false;
                    numTipRatioNom.Enabled = false;
                    numWindSpeedCutIn.Enabled = false;
                    numTipRatioCutIn.Enabled = false;
                    numMaximumPowerCoefficient.Enabled = false;
                    numAirDensity.Enabled = false;
                    lblTurbineRotorRadiusValue.Enabled = false;
                    numRpmMax.Enabled = true;
                    numRpmMin.Enabled = false;
                }
            }
        }
        #endregion
    }
}