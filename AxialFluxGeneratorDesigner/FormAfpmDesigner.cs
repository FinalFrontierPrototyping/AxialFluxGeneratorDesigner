using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using AxialFluxGeneratorDesigner.Calculations;

namespace AxialFluxGeneratorDesigner
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
        ///     The object that are used to display the 3D model
        /// </summary>
        //private read-only HelixViewport3D _generatorView = new HelixViewport3D();
        //elementHost1.Child = _generatorView;

        /// <summary>
        ///     The data table that is used to hold the iterated data
        /// </summary>
        //private read-only DataTable _tableGeneratorSummary = new DataTable();

        /// <summary>
        ///     Check if the form GUI components are initialized.
        /// </summary>
        private bool _isInitialized;

        /// <summary>
        ///     This timer updates the calculations automatically
        ///     The timer is implemented to overcome calculations problems
        ///     TODO: Add more info
        /// </summary>
        private readonly Timer _guiUpdateTimer = new Timer();

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
            //Prevent trigger of event handlers
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

            //Set Energy storage and Front end selection and update combo-boxes
            cmbGeneratorFrontEnd.SelectedIndex = 0;
            cmbEnergyStorage.SelectedIndex = 0;
            _generator.GeneratorEnergyStorageConnection = 0;
            _generator.GeneratorFrontEnd = 0;
            UpdateComboBoxes();

            //Set magnet properties
            foreach (var element in _generator.MagnetProperties)
            {
                cmbMagnetGrade.Items.Add(element.Item1);
            }
            cmbMagnetGrade.SelectedIndex = 3;
            UpdateMagnetProperties();

            //Set default values of numeric up/down controls
            numEnergyStorageVoltageMin.Value = (decimal)_generator.DcVoltageMin;
            numEnergyStorageVoltageMax.Value = (decimal)_generator.DcVoltageMax;
            numEnergyStoragePower.Value = (decimal)_generator.GeneratorPower;
            
            //Front end
            numWindSpeedNom.Value = (decimal) _generator.TurbineWindspeedMax;
            numTipRatioNom.Value = (decimal)_generator.TurbineSpeedTipRatioMax;
            numRpmMax.Value = _generator.FrontEndRpmMax;
            numTipRatioCutIn.Value = (decimal) _generator.TurbineSpeedTipRatioMin;
            numWindSpeedCutIn.Value = (decimal)_generator.TurbineWindspeedMin;
            numRpmMin.Value = _generator.FrontEndRpmMin;
            numMaximumPowerCoefficient.Value = (decimal)_generator.TurbineMaximumPowerCoefficient;
            numAirDensity.Value = (decimal)_generator.TurbineAirDensity;

            //Generator
            numMechanicalGap.Value = (decimal)_generator.MechamicalGap;
            numGeneratorEfficiency.Value = (decimal)_generator.GeneratorEfficiency * 100;

            //Stator
            numCoilsPerPhaseCount.Value = _generator.CoilsPerPhase;

            //Coil
            numCoilFillFactor.Value = (decimal)_generator.CoilFillFactor;
            numMaxCoilCurrentDensity.Value = (decimal)_generator.MaxCurrentDensity;

            //Coil dimensions
            numBetweenCoilDistance.Value = (decimal)_generator.BetweenCoilDistance;

            //Rotor
            numPoleArcToPolePitchRatio.Value = (decimal) _generator.MagnetPoleArcToPolePitchRatio;
            numMagnetThickness.Value = (decimal)_generator.MagnetThickness;
            numMagnetHeight.Value = (decimal)_generator.MagnetHeight;
            numMagnetWidth.Value = (decimal)_generator.MagnetWidth;

            //Rectifier
            numPhaseWireLength.Value = (decimal) _generator.PhaseWireLength;
            numPhaseWireDiameter.Value = (decimal) _generator.PhaseWireDiameter;
            numRectifierWireLength.Value = (decimal) _generator.RectifierWireLength;
            numRectifierWireDiameter.Value = (decimal) _generator.RectifierWireDiameter;

            _isInitialized = true;

            // Initially update calculations and update GUI
            _generator.UpdateCalculations(false);
            UpdateGuiElements();

            //Start GUI timer
            _guiUpdateTimer.Tick += GuiUpdateTimerOnTick;
            _guiUpdateTimer.Interval = 500;
            _guiUpdateTimer.Enabled = true;
        }

        private void GuiUpdateTimerOnTick(object sender, EventArgs eventArgs)
        {
            _generator.UpdateCalculations(false);
            UpdateGuiElements();
        }

        /// <summary>
        /// This method updates all GUI elements 
        /// </summary>
        private void UpdateGuiElements()
        {
            //Front end GUI
            numWindSpeedCutIn.Value = (decimal)Math.Round(_generator.TurbineWindspeedMin, 2);
            numRpmMin.Value = _generator.FrontEndRpmMin;
            numRpmMax.Value = _generator.FrontEndRpmMax;

            lblTurbineRotorRadiusValue.Text =
                Math.Round(_generator.TurbineRotorRadius, 2).ToString(CultureInfo.InvariantCulture);
            lblTurbineMaximalTorqueValue.Text =
                Math.Round(_generator.FrontEndTorque, 2).ToString(CultureInfo.InvariantCulture);

            //Generator requirements
            lblPhaseVoltageMinValue.Text =
                Math.Round(_generator.PhaseVoltageMin, 2).ToString(CultureInfo.InvariantCulture);
            lblPhaseVoltageMaxValue.Text =
                Math.Round(_generator.PhaseVoltageMax, 2).ToString(CultureInfo.InvariantCulture);
            lblMaxPhaseCurrentValue.Text =
                Math.Round(_generator.MaxPhaseCurrent, 2).ToString(CultureInfo.InvariantCulture);

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

        private void cmbMagnetGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMagnetProperties();
        }

        private void UpdateMagnetProperties()
        {
            _generator.MagnetGrade = _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item1;
            _generator.MagnetRemanentFluxDensity = _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item2;
            _generator.MagnetCoerciveFieldStrength = _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item3;

            lblMagnetRemanenceValue.Text =
                _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item2.ToString(CultureInfo.InvariantCulture);
            lblCoerciveFieldStrengthValue.Text =
                _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item3.ToString(CultureInfo.InvariantCulture);
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

        #region Numeric up down events

        private void numDCVoltage_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.DcVoltageMax = (double) numEnergyStorageVoltageMax.Value;
        }

        private void numCoilsPerPhaseCount_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.CoilsPerPhase = (int) numCoilsPerPhaseCount.Value;
        }

        private void numlMagnetThickness_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.MagnetThickness = (double) numMagnetThickness.Value;
        }

        private void numMagnetWidth_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.MagnetWidth = (double) numMagnetWidth.Value;
        }

        private void numMagnetLength_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.MagnetHeight = (double) numMagnetHeight.Value;
        }

        private void numMechanicalGap_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.MechamicalGap = (double) numMechanicalGap.Value;
        }

        private void numBetweenCoilDistance_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.BetweenCoilDistance = (double) numBetweenCoilDistance.Value;
        }

        private void numShaftPower_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.GeneratorPower = (int) numEnergyStoragePower.Value;
        }

        private void numWindSpeedNom_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.TurbineWindspeedMax = (double) numWindSpeedNom.Value;
        }

        private void numTipRatioNom_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.TurbineSpeedTipRatioMax = (double) numTipRatioNom.Value;
        }

        private void numInverterVoltageMin_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.DcVoltageMin = (double) numEnergyStorageVoltageMin.Value;
        }

        private void numTipRatioCutIn_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.TurbineSpeedTipRatioMin = (double) numTipRatioCutIn.Value;
        }

        private void numMaximumPowerCoefficient_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.TurbineMaximumPowerCoefficient = (double) numMaximumPowerCoefficient.Value;
        }

        private void numAirDensity_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.TurbineAirDensity = (double) numAirDensity.Value;
        }

        private void numWindSpeedCutIn_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.TurbineWindspeedMin = (double) numWindSpeedCutIn.Value;
        }

        private void numCoilFillFactor_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.CoilFillFactor = (double) numCoilFillFactor.Value;
        }

        private void numCoilHeatCoefficient_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.MaxCurrentDensity = (double) numMaxCoilCurrentDensity.Value;
        }

        private void numGeneratorEfficiency_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.GeneratorEfficiency = (double) (numGeneratorEfficiency.Value/100);
        }

        private void numPhaseWireLength_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.PhaseWireLength = (double) numPhaseWireLength.Value;
        }

        private void numPhaseWireDiameter_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.PhaseWireDiameter = (double) numPhaseWireDiameter.Value;
        }

        private void numRectifierWireLength_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.RectifierWireLength = (double) numRectifierWireLength.Value;
        }

        private void numRectifierWireDiameter_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.RectifierWireDiameter = (double) numRectifierWireDiameter.Value;
        }

        private void numRpmMax_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.FrontEndRpmMax = (int) numRpmMax.Value;
        }

        private void numRpmMin_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.FrontEndRpmMin = (int) numRpmMin.Value;
        }

        private void numPoleArcToPolePitchRatio_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            _generator.MagnetPoleArcToPolePitchRatio = (double)numPoleArcToPolePitchRatio.Value;
        }
        #endregion

        #region Front end combo box handling

        private void cmbEnergyStorage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            UpdateComboBoxes();
        }

        private void cmbGeneratorFrontEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isInitialized) return;
            UpdateComboBoxes();
        }

        /// <summary>
        /// </summary>
        private void UpdateComboBoxes()
        {
            //Battery
            if (cmbEnergyStorage.SelectedIndex == 0)
            {
                _generator.GeneratorEnergyStorageConnection = 0;
                lblInverterVoltageMin.Text = @"Battery voltage (V)";
                numEnergyStorageVoltageMax.Visible = false;

                //Wind turbine + Battery
                if (cmbGeneratorFrontEnd.SelectedIndex == 0)
                {
                    _generator.GeneratorFrontEnd = 0;
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

                    //TODO: Move to iterator class
                    numIterateDcVoltageBatteryMinLower.Enabled = true;
                    numIterateDcVoltageBatteryMinUpper.Enabled = true;
                    numIterateDcVoltageBatteryMaxLower.Enabled = false;
                    numIterateDcVoltageBatteryMaxUpper.Enabled = false;

                    numWindSpeedMinLower.Enabled = true;
                    numWindSpeedMinUpper.Enabled = true;
                    numSpeedTipRatioMinLower.Enabled = true;
                    numSpeedTipRatioMinUpper.Enabled = true;

                    numWindSpeedMaxLower.Enabled = true;
                    numWindSpeedMaxUpper.Enabled = true;
                    numSpeedTipRatioMaxLower.Enabled = true;
                    numSpeedTipRatioMaxUpper.Enabled = true;

                    numMaximumPowerCoefficientLower.Enabled = true;
                    numMaximumPowerCoefficientUpper.Enabled = true;
                    numAirDensityLower.Enabled = true;
                    numAirDensityUpper.Enabled = true;

                    numOtherRpmMinLower.Enabled = false;
                    numOtherRpmMinUpper.Enabled = false;
                    numOtherRpmMaxLower.Enabled = false;
                    numOtherRpmMaxUpper.Enabled = false;
                }
                //Other + Battery
                else if (cmbGeneratorFrontEnd.SelectedIndex == 1)
                {
                    _generator.GeneratorFrontEnd = 1;
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

                    //Iterate page
                    numIterateDcVoltageBatteryMinLower.Enabled = true;
                    numIterateDcVoltageBatteryMinUpper.Enabled = true;
                    numIterateDcVoltageBatteryMaxLower.Enabled = false;
                    numIterateDcVoltageBatteryMaxUpper.Enabled = false;

                    numWindSpeedMinLower.Enabled = false;
                    numWindSpeedMinUpper.Enabled = false;
                    numSpeedTipRatioMinLower.Enabled = false;
                    numSpeedTipRatioMinUpper.Enabled = false;

                    numWindSpeedMaxLower.Enabled = false;
                    numWindSpeedMaxUpper.Enabled = false;
                    numSpeedTipRatioMaxLower.Enabled = false;
                    numSpeedTipRatioMaxUpper.Enabled = false;

                    numMaximumPowerCoefficientLower.Enabled = false;
                    numMaximumPowerCoefficientUpper.Enabled = false;
                    numAirDensityLower.Enabled = false;
                    numAirDensityUpper.Enabled = false;

                    numOtherRpmMinLower.Enabled = true;
                    numOtherRpmMinUpper.Enabled = true;
                    numOtherRpmMaxLower.Enabled = true;
                    numOtherRpmMaxUpper.Enabled = true;
                }
            }
            //Grid
            else if (cmbEnergyStorage.SelectedIndex == 1)
            {
                _generator.GeneratorEnergyStorageConnection = 1;
                lblInverterVoltageMin.Text = @"Minimal voltage (V)";
                numEnergyStorageVoltageMax.Visible = true;

                //Wind turbine + Grid
                if (cmbGeneratorFrontEnd.SelectedIndex == 0)
                {
                    _generator.GeneratorFrontEnd = 0;
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

                    //Iterate page
                    numIterateDcVoltageBatteryMinLower.Enabled = true;
                    numIterateDcVoltageBatteryMinUpper.Enabled = true;
                    numIterateDcVoltageBatteryMaxLower.Enabled = true;
                    numIterateDcVoltageBatteryMaxUpper.Enabled = true;

                    numWindSpeedMinLower.Enabled = false;
                    numWindSpeedMinUpper.Enabled = false;
                    numSpeedTipRatioMinLower.Enabled = true;
                    numSpeedTipRatioMinUpper.Enabled = true;

                    numWindSpeedMaxLower.Enabled = true;
                    numWindSpeedMaxUpper.Enabled = true;
                    numSpeedTipRatioMaxLower.Enabled = true;
                    numSpeedTipRatioMaxUpper.Enabled = true;

                    numMaximumPowerCoefficientLower.Enabled = true;
                    numMaximumPowerCoefficientUpper.Enabled = true;
                    numAirDensityLower.Enabled = true;
                    numAirDensityUpper.Enabled = true;

                    numOtherRpmMinLower.Enabled = false;
                    numOtherRpmMinUpper.Enabled = false;
                    numOtherRpmMaxLower.Enabled = false;
                    numOtherRpmMaxUpper.Enabled = false;
                }
                //Other + Grid
                else if (cmbGeneratorFrontEnd.SelectedIndex == 1)
                {
                    _generator.GeneratorFrontEnd = 1;
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

                    //Iterate page
                    numIterateDcVoltageBatteryMinLower.Enabled = true;
                    numIterateDcVoltageBatteryMinUpper.Enabled = true;
                    numIterateDcVoltageBatteryMaxLower.Enabled = true;
                    numIterateDcVoltageBatteryMaxUpper.Enabled = true;

                    numWindSpeedMinLower.Enabled = false;
                    numWindSpeedMinUpper.Enabled = false;
                    numSpeedTipRatioMinLower.Enabled = false;
                    numSpeedTipRatioMinUpper.Enabled = false;

                    numWindSpeedMaxLower.Enabled = false;
                    numWindSpeedMaxUpper.Enabled = false;
                    numSpeedTipRatioMaxLower.Enabled = false;
                    numSpeedTipRatioMaxUpper.Enabled = false;

                    numMaximumPowerCoefficientLower.Enabled = false;
                    numMaximumPowerCoefficientUpper.Enabled = false;
                    numAirDensityLower.Enabled = false;
                    numAirDensityUpper.Enabled = false;

                    numOtherRpmMinLower.Enabled = false;
                    numOtherRpmMinUpper.Enabled = false;
                    numOtherRpmMaxLower.Enabled = true;
                    numOtherRpmMaxUpper.Enabled = true;
                }
            }
        }

        private void tabDesigner_Click(object sender, EventArgs e)
        {

        }

        #endregion

        /*
        //GenerateImage(tlpEnergyStorageConnection);
        //GenerateImage(tbpTurbine);
        //GenerateImage(tlpStatorCoils);
        //GenerateImage(tableLayoutPanel1);
        //GenerateImage(tableLayoutPanel2);
        //GenerateImage(tableLayoutPanel3);
        //GenerateImage(tableLayoutPanel4);
        //GenerateImage(tableLayoutPanel7);

        private void GenerateImage(Control panel)
        {
            //Save control images for documentation purposes
            var image = new Bitmap(panel.ClientRectangle.Width, panel.ClientRectangle.Height);
            panel.DrawToBitmap(image, panel.ClientRectangle);
            image.Save(panel.Name + ".png");
        }
        */
    }
}