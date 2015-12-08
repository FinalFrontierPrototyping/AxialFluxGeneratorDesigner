using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Media.Media3D;
using System.Windows.Input;

namespace AxialFluxGeneratorDesigner
{
    /// <summary>
    /// This is the GUI part of the AFPMG Designer
    /// </summary>
    public partial class FormAfpmDesigner : Form
    {
        /// <summary>
        /// Check if the form GUI components are initialized.
        /// </summary>
        public bool IsInitialized = false;

        /// <summary>
        /// The Class containing the code to perform calculations
        /// </summary>
        private readonly Afpm _generator = new Afpm();

        /// <summary>
        /// The object that are used to display the 3D model 
        /// </summary>
        private readonly HelixViewport3D _generatorView = new HelixViewport3D();

        private ModelVisual3D device3D = new ModelVisual3D();

        /// <summary>
        /// The amount of iteration steps that are done.
        /// </summary>
        private const int Iterations = 100;

        /// <summary>
        /// The data table that is used to hold the iterated data
        /// </summary>
        private readonly DataTable _tableGeneratorSummary = new DataTable();

        /// <summary>
        ///The constructor of the Form class.
        /// </summary>
        public FormAfpmDesigner()
        {
            InitializeComponent();
            tabGenerator.SelectedIndexChanged += Tabs_SelectedIndexChanged;
        }

        /// <summary>
        /// The method that is triggered if the index of the selected tab is changed.
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
                numIterateDcVoltageBatteryMinLower.Value = (decimal) _generator.DcVoltageMin;
                numIterateDcVoltageBatteryMinUpper.Value = (decimal) _generator.DcVoltageMin;
                numIterateDcVoltageBatteryMaxLower.Value = (decimal) _generator.DcVoltageMax;
                numIterateDcVoltageBatteryMaxUpper.Value = (decimal) _generator.DcVoltageMax;
                numShaftPowerLower.Value = (decimal) _generator.GeneratorPower;
                numShaftPowerUpper.Value = (decimal) _generator.GeneratorPower;
                numWindSpeedMaxLower.Value = (decimal) _generator.TurbineWindspeedMax;
                numWindSpeedMaxUpper.Value = (decimal) _generator.TurbineWindspeedMax;
                numSpeedTipRatioMaxLower.Value = (decimal) _generator.TurbineSpeedTipRatioMax;
                numSpeedTipRatioMaxUpper.Value = (decimal) _generator.TurbineSpeedTipRatioMax;
                numWindSpeedMinLower.Value = (decimal) _generator.TurbineWindspeedMin;
                numWindSpeedMinUpper.Value = (decimal) _generator.TurbineWindspeedMin;
                numSpeedTipRatioMinLower.Value = (decimal) _generator.TurbineSpeedTipRatioMin;
                numSpeedTipRatioMinUpper.Value = (decimal) _generator.TurbineSpeedTipRatioMin;
                numMaximumPowerCoefficientLower.Value = (decimal) _generator.TurbineMaximumPowerCoefficient;
                numMaximumPowerCoefficientUpper.Value = (decimal) _generator.TurbineMaximumPowerCoefficient;
                numAirDensityLower.Value = (decimal) _generator.TurbineAirDensity;
                numAirDensityUpper.Value = (decimal) _generator.TurbineAirDensity;
                numOtherRpmMinLower.Value = _generator.FrontEndRpmMin;
                numOtherRpmMinUpper.Value = _generator.FrontEndRpmMin;
                numOtherRpmMaxLower.Value = _generator.FrontEndRpmMax;
                numOtherRpmMaxUpper.Value = _generator.FrontEndRpmMax;
                numMechanicalGapLower.Value = (decimal) _generator.MechamicalGap;
                numMechanicalGapUpper.Value = (decimal) _generator.MechamicalGap;
                numGeneratorEfficiencyLower.Value = (decimal) _generator.GeneratorEfficiency;
                numGeneratorEfficiencyUpper.Value = (decimal) _generator.GeneratorEfficiency;
                numCoilHeatCoefficientLower.Value = (decimal) _generator.CoilHeatCoefficient;
                numCoilHeatCoefficientUpper.Value = (decimal) _generator.CoilHeatCoefficient;
                numCoilFillFactorLower.Value = (decimal) _generator.CoilFillFactor;
                numCoilFillFactorUpper.Value = (decimal) _generator.CoilFillFactor;
                numMagnetThicknessLower.Value = (decimal) _generator.MagnetThickness;
                numMagnetThicknessUpper.Value = (decimal) _generator.MagnetThickness;
                numMagnetLengthLower.Value = (decimal) _generator.MagnetLength;
                numMagnetLengthUpper.Value = (decimal) _generator.MagnetLength;
                numMagnetWidthLower.Value = (decimal) _generator.MagnetWidth;
                numMagnetWidthUpper.Value = (decimal) _generator.MagnetWidth;
                numDiodeVoltageDropLower.Value = (decimal) _generator.RectifierDiodeVoltageDrop;
                numDiodeVoltageDropUpper.Value = (decimal) _generator.RectifierDiodeVoltageDrop;
                numPhaseWireLengthLower.Value = (decimal) _generator.PhaseWireLength;
                numPhaseWireLengthUpper.Value = (decimal) _generator.PhaseWireLength;
                numPhaseWireDiameterLower.Value = (decimal) _generator.PhaseWireDiameter;
                numPhaseWireDiameterUpper.Value = (decimal) _generator.PhaseWireDiameter;
                numRectifierWireLengthLower.Value = (decimal) _generator.RectifierWireLength;
                numRectifierWireLengthUpper.Value = (decimal) _generator.RectifierWireLength;
                numRectifierWireDiameterLower.Value = (decimal) _generator.RectifierWireDiameter;
                numRectifierWireDiameterUpper.Value = (decimal) _generator.RectifierWireDiameter;
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
        /// This method is called when the form is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            elementHost1.Child = _generatorView;

            foreach (var element in _generator.MagnetProperties)
            {
                cmbMagnetGrade.Items.Add(element.Item1);
            }
            cmbMagnetGrade.SelectedIndex = 3;

            _generator.MagnetGrade = _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item1;

            numIterateDcVoltageBatteryMinLower.Value = (decimal) _generator.DcVoltageMin;
            numIterateDcVoltageBatteryMinUpper.Value = (decimal) _generator.DcVoltageMin;

            dgvGeneratorSummary.RowTemplate.Height = 18;

            _tableGeneratorSummary.Columns.Add("Variable", typeof (string));

            _tableGeneratorSummary.Rows.Add("Minimal DC voltage (V)");
            _tableGeneratorSummary.Rows.Add("Maximal DC voltage (V)");
            _tableGeneratorSummary.Rows.Add("Power (W)");

            _tableGeneratorSummary.Rows.Add("Wind speed max (m/s)");
            _tableGeneratorSummary.Rows.Add("Tip speed ratio max");
            _tableGeneratorSummary.Rows.Add("Wind speed min (m/s)");
            _tableGeneratorSummary.Rows.Add("Tip speed ratio min");
            _tableGeneratorSummary.Rows.Add("Rotor radius (m)");
            _tableGeneratorSummary.Rows.Add("Maximum power coefficient");
            _tableGeneratorSummary.Rows.Add("Air density (kg/m3)");

            _tableGeneratorSummary.Rows.Add("RPM max");
            _tableGeneratorSummary.Rows.Add("RPM min");
            _tableGeneratorSummary.Rows.Add("Maximum torque (Nm)");

            _tableGeneratorSummary.Rows.Add("Minimal phase voltage (V)");
            _tableGeneratorSummary.Rows.Add("Maximum phase voltage (V)");
            _tableGeneratorSummary.Rows.Add("Maximum phase current (A)");
            _tableGeneratorSummary.Rows.Add("Mechanical gap (mm)");
            _tableGeneratorSummary.Rows.Add("Generator efficiency (%)");

            _tableGeneratorSummary.Rows.Add("Phase count");
            _tableGeneratorSummary.Rows.Add("Coils per phase");
            _tableGeneratorSummary.Rows.Add("Coils count");
            _tableGeneratorSummary.Rows.Add("Coil thickness (mm)");
            _tableGeneratorSummary.Rows.Add("Coil leg width (mm)");
            _tableGeneratorSummary.Rows.Add("Coil heat coeff. (W/cm2)");
            _tableGeneratorSummary.Rows.Add("Coil Fill factor");
            _tableGeneratorSummary.Rows.Add("Coil Turns");
            _tableGeneratorSummary.Rows.Add("Coil winding factor");
            _tableGeneratorSummary.Rows.Add("Coil wire diameter (mm)");
            _tableGeneratorSummary.Rows.Add("Coil max current density");
            _tableGeneratorSummary.Rows.Add("Coil wire length (m)");
            _tableGeneratorSummary.Rows.Add("Coil resistance (Ohm)");
            _tableGeneratorSummary.Rows.Add("Coil Inductance (mH)");

            _tableGeneratorSummary.Rows.Add("Rotor thickness (mm)");
            _tableGeneratorSummary.Rows.Add("Rotor outer radius (mm)");
            _tableGeneratorSummary.Rows.Add("Rotor inner radius (mm)");
            _tableGeneratorSummary.Rows.Add("Rotor radius ratio");
            _tableGeneratorSummary.Rows.Add("Rotor magnet count");

            _tableGeneratorSummary.Rows.Add("Magnet thickness (mm)");
            _tableGeneratorSummary.Rows.Add("Magnet width (mm)");
            _tableGeneratorSummary.Rows.Add("Magnet length (mm)");
            _tableGeneratorSummary.Rows.Add("Magnet remanence (T)");
            _tableGeneratorSummary.Rows.Add("Magnet field strength(kA/m)");
            _tableGeneratorSummary.Rows.Add("Magnet flux density (T)");
            _tableGeneratorSummary.Rows.Add("Magnet coil (T)");
            _tableGeneratorSummary.Rows.Add("Magnet distance (mm)");
            _tableGeneratorSummary.Rows.Add("Magnet Pole arc / Pole pitch");

            _tableGeneratorSummary.Rows.Add("Diode voltage drop (V)");
            _tableGeneratorSummary.Rows.Add("Phase wire length (m)");
            _tableGeneratorSummary.Rows.Add("Phase wire diameter (mm)");
            _tableGeneratorSummary.Rows.Add("Voltage drop (V)");
            _tableGeneratorSummary.Rows.Add("Wire resistance (Ohm)");
            _tableGeneratorSummary.Rows.Add("Rectifier wire length (m)");
            _tableGeneratorSummary.Rows.Add("Rectifier wire diameter (mm)");
            _tableGeneratorSummary.Rows.Add("Rectifier wire voltage drop (V)");
            _tableGeneratorSummary.Rows.Add("Rectifier wire resistance (Ohm)");

            for (int i = 0; i < Iterations + 1; i++)
            {
                _tableGeneratorSummary.Columns.Add("Iteration " + (i + 1), typeof (double));
            }

            dgvGeneratorSummary.DataSource = _tableGeneratorSummary;
            dgvGeneratorSummary.Columns[0].Width = 190;

            var gridViewColumn = dgvGeneratorSummary.Columns["Variable"];
            if (gridViewColumn != null) gridViewColumn.Frozen = true;

            foreach (DataRow row in _tableGeneratorSummary.Rows)
            {
                cmbChart1xAxis.Items.Add(row[0]);
                cmbChart1yAxis.Items.Add(row[0]);
                cmbChart2xAxis.Items.Add(row[0]);
                cmbChart2yAxis.Items.Add(row[0]);
            }

            cmbChart1xAxis.SelectedIndex = 0;
            cmbChart1yAxis.SelectedIndex = 0;
            cmbChart2xAxis.SelectedIndex = 0;
            cmbChart2yAxis.SelectedIndex = 0;

            cmbEnergyStorage.Items.Add("Battery connection");
            cmbEnergyStorage.Items.Add("Grid connection");
            cmbEnergyStorage.SelectedIndex = 0;

            cmbGeneratorFrontEnd.Items.Add("Wind turbine");
            cmbGeneratorFrontEnd.Items.Add("Other");
            cmbGeneratorFrontEnd.SelectedIndex = 0;

            _generator.GeneratorEnergyStorageConnection = 0;
            _generator.GeneratorFrontEnd = 0;
            UpdateComboBoxes();

            _generator.MagnetRemanentFluxDensity = _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item2;
            _generator.MagnetCoerciveFieldStrength = _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item3;

            lblMagnetRemanenceValue.Text =
                _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item2.ToString(CultureInfo.InvariantCulture);
            lblCoerciveFieldStrengthValue.Text =
                _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item3.ToString(CultureInfo.InvariantCulture);

            numCoilWindingCoefficient.Value = (decimal) _generator.CoilWindingCoefficient;
            numAirDensity.Value = (decimal) _generator.TurbineAirDensity;
            numVoltageMin.Value = (decimal) _generator.DcVoltageMin;
            numInverterVoltageMax.Value = (decimal) _generator.DcVoltageMax;
            numWindSpeedNom.Value = (decimal) _generator.TurbineWindspeedMax;
            numTipRatioCutIn.Value = (decimal) _generator.TurbineSpeedTipRatioMin;
            numTipRatioNom.Value = (decimal) _generator.TurbineSpeedTipRatioMax;
            numMaximumPowerCoefficient.Value = (decimal) _generator.TurbineMaximumPowerCoefficient;
            numMagnetThickness.Value = (decimal) _generator.MagnetThickness;
            numMagnetLength.Value = (decimal) _generator.MagnetLength;
            numMagnetWidth.Value = (decimal) _generator.MagnetWidth;
            numCoilsPerPhaseCount.Value = _generator.CoilsPerPhase;
            numMechanicalGap.Value = (decimal) _generator.MechamicalGap;
            numShaftPower.Value = (decimal) _generator.GeneratorPower;
            numWindSpeedCutIn.Value = (decimal) _generator.TurbineWindspeedMin;
            numCoilFillFactor.Value = (decimal) _generator.CoilFillFactor;
            numCoilHeatCoefficient.Value = (decimal) _generator.CoilHeatCoefficient;
            numGeneratorEfficiency.Value = (decimal) _generator.GeneratorEfficiency*100;
            numPhaseWireLength.Value = (decimal) _generator.PhaseWireLength;
            numPhaseWireDiameter.Value = (decimal) _generator.PhaseWireDiameter;
            numRectifierWireLength.Value = (decimal) _generator.RectifierWireLength;
            numRectifierWireDiameter.Value = (decimal) _generator.RectifierWireDiameter;
            numRectifierDiodeVoltageDrop.Value = (decimal) _generator.RectifierDiodeVoltageDrop;

            numRpmMin.Value = _generator.FrontEndRpmMin;
            numRpmMax.Value = _generator.FrontEndRpmMax;

            _generator.MagnetRemanentFluxDensity = _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item2;
            _generator.MagnetCoerciveFieldStrength = _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item3;

            IsInitialized = true;

            UpdateTrigger();

            //FillIterationTable();

            //GenerateImage(tlpEnergyStorageConnection);
            //GenerateImage(tbpTurbine);
            //GenerateImage(tlpStatorCoils);
            //GenerateImage(tableLayoutPanel1);
            //GenerateImage(tableLayoutPanel2);
            //GenerateImage(tableLayoutPanel3);
            //GenerateImage(tableLayoutPanel4);
            //GenerateImage(tableLayoutPanel7);
        }

        private void UpdateGuiElements()
        {
            lblCoilLegWidthValue.Text = Math.Round(_generator.CoilLegWidth, 2).ToString(CultureInfo.InvariantCulture);
            lblStatorThicknessValue.Text = Math.Round(_generator.CoilThickness, 2)
                .ToString(CultureInfo.InvariantCulture);
            lblCoilTurnsValue.Text = _generator.CoilTurns.ToString();
            lblMagnetFluxDensityValue.Text =
                Math.Round(_generator.MagnetFluxDensity, 2).ToString(CultureInfo.InvariantCulture);
            lblMagnetFluxValue.Text =
                Math.Round(_generator.MagnetPoleFlux*1000, 2).ToString(CultureInfo.InvariantCulture);
            lblMaxCoilCurrentDensityValue.Text =
                Math.Round(_generator.MaxCurrentDensity, 2).ToString(CultureInfo.InvariantCulture);
            lblCoilWireDiameterValue.Text =
                Math.Round(_generator.CoilWireDiameter, 2).ToString(CultureInfo.InvariantCulture);
            lblRotorOuterRadiusValue.Text =
                Math.Round(_generator.RotorOuterRadius, 2).ToString(CultureInfo.InvariantCulture);
            lblRotorInnerRadiusValue.Text =
                Math.Round(_generator.RotorInnerRadius, 2).ToString(CultureInfo.InvariantCulture);
            lblRotorInnerouterRadiusRatioValue.Text =
                Math.Round(_generator.RotorInnerOuterRadiusRatio, 2).ToString(CultureInfo.InvariantCulture);
            lblRotorThicknessValue.Text = Math.Round(_generator.RotorThickness, 2)
                .ToString(CultureInfo.InvariantCulture);
            lblPhaseVoltageMinValue.Text =
                Math.Round(_generator.PhaseVoltageMin, 2).ToString(CultureInfo.InvariantCulture);
            lblPhaseVoltageMaxValue.Text =
                Math.Round(_generator.PhaseVoltageMax, 2).ToString(CultureInfo.InvariantCulture);
            lblMagnetCountValue.Text = _generator.MagnetCount.ToString();
            lblCoilCountValue.Text = _generator.CoilCount.ToString();
            lblMagnetDistanceValue.Text = Math.Round(_generator.MagnetDistance, 2)
                .ToString(CultureInfo.InvariantCulture);
            lblMagnetArcPolePitchValue.Text =
                Math.Round(_generator.MagnetPoleArcPitch, 2).ToString(CultureInfo.InvariantCulture);
            lblMaxPhaseCurrentValue.Text =
                Math.Round(_generator.MaxPhaseCurrent, 2).ToString(CultureInfo.InvariantCulture);
            numWindSpeedCutIn.Value = (decimal) Math.Round(_generator.TurbineWindspeedMin, 2);
            lblTurbineRotorRadiusValue.Text =
                Math.Round(_generator.TurbineRotorRadius, 2).ToString(CultureInfo.InvariantCulture);
            lblCoilResistanceValue.Text = Math.Round(_generator.CoilResistance, 2)
                .ToString(CultureInfo.InvariantCulture);
            lblCoilInductanceValue.Text = Math.Round(_generator.CoilInductance, 2)
                .ToString(CultureInfo.InvariantCulture);
            lblCoilWireLengthValue.Text = Math.Round(_generator.CoilWireLength, 2)
                .ToString(CultureInfo.InvariantCulture);
            lblGeneratorPhaseCountValue.Text = _generator.PhaseCount.ToString();
            lblPhaseWireVoltageDropValue.Text =
                Math.Round(_generator.PhaseWireVoltageDrop, 2).ToString(CultureInfo.InvariantCulture);
            lblPhaseWireResistanceValue.Text =
                Math.Round(_generator.PhaseWireResistance, 2).ToString(CultureInfo.InvariantCulture);
            lblRectifierWireVoltageDropValue.Text =
                Math.Round(_generator.RectifierWireVoltageDrop, 2).ToString(CultureInfo.InvariantCulture);
            lblRectifierWireResistanceValue.Text =
                Math.Round(_generator.RectifierWireResistance, 2).ToString(CultureInfo.InvariantCulture);
            lblTurbineMaximalTorqueValue.Text =
                Math.Round(_generator.FrontEndTorque, 2).ToString(CultureInfo.InvariantCulture);
            numRpmMin.Value = _generator.FrontEndRpmMin;
            numRpmMax.Value = _generator.FrontEndRpmMax;
        }

        private void numDCVoltage_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.DcVoltageMax = (double) numInverterVoltageMax.Value;
            UpdateTrigger();
        }

        private void numCoilsPerPhaseCount_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.CoilsPerPhase = (int) numCoilsPerPhaseCount.Value;
            UpdateTrigger();
        }

        private void numlMagnetThickness_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.MagnetThickness = (double) numMagnetThickness.Value;
            UpdateTrigger();
        }

        private void numMagnetWidth_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.MagnetWidth = (double) numMagnetWidth.Value;
            UpdateTrigger();
        }

        private void numMagnetLength_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.MagnetLength = (double) numMagnetLength.Value;
            UpdateTrigger();
        }

        private void numMechanicalGap_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.MechamicalGap = (double) numMechanicalGap.Value;
            UpdateTrigger();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.MagnetGrade = _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item1;
            _generator.MagnetRemanentFluxDensity = _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item2;
            _generator.MagnetCoerciveFieldStrength = _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item3;

            lblMagnetRemanenceValue.Text =
                _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item2.ToString(CultureInfo.InvariantCulture);
            lblCoerciveFieldStrengthValue.Text =
                _generator.MagnetProperties[cmbMagnetGrade.SelectedIndex].Item3.ToString(CultureInfo.InvariantCulture);

            UpdateTrigger();
        }

        private void numShaftPower_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.GeneratorPower = (int) numShaftPower.Value;
            UpdateTrigger();
        }

        private void numWindSpeedNom_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.TurbineWindspeedMax = (double) numWindSpeedNom.Value;
            UpdateTrigger();
        }

        private void numTipRatioNom_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.TurbineSpeedTipRatioMax = (double) numTipRatioNom.Value;
            UpdateTrigger();
        }

        private void numInverterVoltageMin_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.DcVoltageMin = (double) numVoltageMin.Value;
            UpdateTrigger();
        }

        private void numTipRatioCutIn_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.TurbineSpeedTipRatioMin = (double) numTipRatioCutIn.Value;
            UpdateTrigger();
        }

        private void numMaximumPowerCoefficient_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.TurbineMaximumPowerCoefficient = (double) numMaximumPowerCoefficient.Value;
            UpdateTrigger();
        }

        private void numAirDensity_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.TurbineAirDensity = (double) numAirDensity.Value;
            UpdateTrigger();
        }

        private void UpdateTrigger()
        {
            _generator.UpdateCalculations(true);
            UpdateGuiElements();
        }

        private void numCoilWindingCoefficient_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.CoilWindingCoefficient = (double) numCoilWindingCoefficient.Value;
            UpdateTrigger();
        }

        private void cmbEnergyStorage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            UpdateComboBoxes();
            UpdateTrigger();
        }

        private void cmbGeneratorFrontEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            UpdateComboBoxes();
            UpdateTrigger();
        }

        private void numWindSpeedCutIn_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.TurbineWindspeedMin = (double) numWindSpeedCutIn.Value;
            UpdateTrigger();
        }

        private void numCoilFillFactor_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.CoilFillFactor = (double) numCoilFillFactor.Value;
            UpdateTrigger();
        }

        private void numCoilHeatCoefficient_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.CoilHeatCoefficient = (double) numCoilHeatCoefficient.Value;
            UpdateTrigger();
        }

        private void numGeneratorEfficiency_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.GeneratorEfficiency = (double) (numGeneratorEfficiency.Value/100);
            UpdateTrigger();
        }

        private void numPhaseWireLength_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.PhaseWireLength = (double) numPhaseWireLength.Value;
            UpdateTrigger();
        }

        private void numPhaseWireDiameter_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.PhaseWireDiameter = (double) numPhaseWireDiameter.Value;
            UpdateTrigger();
        }

        private void numRectifierWireLength_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.RectifierWireLength = (double) numRectifierWireLength.Value;
            UpdateTrigger();
        }

        private void numRectifierWireDiameter_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.RectifierWireDiameter = (double) numRectifierWireDiameter.Value;
            UpdateTrigger();
        }

        private void numRectifierDiodeVoltageDrop_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.RectifierDiodeVoltageDrop = (double) numRectifierDiodeVoltageDrop.Value;
            UpdateTrigger();
        }

        private void numRpmMax_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.FrontEndRpmMax = (int) numRpmMax.Value;
            UpdateTrigger();
        }

        private void numRpmMin_ValueChanged(object sender, EventArgs e)
        {
            if (!IsInitialized) return;
            _generator.FrontEndRpmMin = (int) numRpmMin.Value;
            UpdateTrigger();
        }

        /// <summary>
        ///
        /// </summary>
        public double IterableCalculate(NumericUpDown lower, NumericUpDown upper, int iteration)
        {
            return ((double) lower.Value + (((double) upper.Value - (double) lower.Value)/Iterations)*iteration);
        }

        /// <summary>
        ///
        /// </summary>
        public void FillIterationTable()
        {
            for (var i = 0; i < 100 + 1; i++)
            {
                _generator.DcVoltageMin = IterableCalculate(numIterateDcVoltageBatteryMinLower,
                    numIterateDcVoltageBatteryMinUpper, i);
                _generator.DcVoltageMax = IterableCalculate(numIterateDcVoltageBatteryMaxLower,
                    numIterateDcVoltageBatteryMaxUpper, i);
                _generator.GeneratorPower = IterableCalculate(numShaftPowerLower, numShaftPowerUpper, i);

                _generator.TurbineWindspeedMax = IterableCalculate(numWindSpeedMaxLower, numWindSpeedMaxUpper, i);
                _generator.TurbineSpeedTipRatioMax = IterableCalculate(numSpeedTipRatioMaxLower,
                    numSpeedTipRatioMaxUpper, i);
                _generator.TurbineWindspeedMin = IterableCalculate(numWindSpeedMinLower, numWindSpeedMinUpper, i);
                _generator.TurbineSpeedTipRatioMin = IterableCalculate(numSpeedTipRatioMinLower,
                    numSpeedTipRatioMinUpper, i);
                _generator.TurbineMaximumPowerCoefficient = IterableCalculate(numMaximumPowerCoefficientLower,
                    numMaximumPowerCoefficientUpper, i);
                _generator.TurbineAirDensity = IterableCalculate(numAirDensityLower, numAirDensityUpper, i);

                _generator.FrontEndRpmMin = (int) IterableCalculate(numOtherRpmMinLower, numOtherRpmMinUpper, i);
                _generator.FrontEndRpmMax = (int) IterableCalculate(numOtherRpmMaxLower, numOtherRpmMaxUpper, i);

                _generator.MechamicalGap = IterableCalculate(numMechanicalGapLower, numMechanicalGapUpper, i);
                _generator.GeneratorEfficiency = IterableCalculate(numGeneratorEfficiencyLower,
                    numGeneratorEfficiencyUpper, i);
                _generator.CoilHeatCoefficient = IterableCalculate(numCoilHeatCoefficientLower,
                    numCoilHeatCoefficientUpper, i);
                _generator.CoilFillFactor = IterableCalculate(numCoilFillFactorLower, numCoilFillFactorUpper, i);

                _generator.MagnetThickness = IterableCalculate(numMagnetThicknessLower, numMagnetThicknessUpper, i);
                _generator.MagnetLength = IterableCalculate(numMagnetLengthLower, numMagnetLengthUpper, i);
                _generator.MagnetWidth = IterableCalculate(numMagnetWidthLower, numMagnetWidthUpper, i);

                _generator.RectifierDiodeVoltageDrop = IterableCalculate(numDiodeVoltageDropLower,
                    numDiodeVoltageDropUpper, i);
                _generator.PhaseWireLength = IterableCalculate(numPhaseWireLengthLower, numPhaseWireLengthUpper, i);
                _generator.PhaseWireDiameter = IterableCalculate(numPhaseWireDiameterLower, numPhaseWireDiameterUpper, i);
                _generator.RectifierWireLength = IterableCalculate(numRectifierWireLengthLower,
                    numRectifierWireLengthUpper, i);
                _generator.RectifierWireDiameter = IterableCalculate(numRectifierWireDiameterLower,
                    numRectifierWireDiameterUpper, i);

                _generator.UpdateCalculations(true);

                _tableGeneratorSummary.Rows[0][i + 1] = _generator.DcVoltageMin;
                _tableGeneratorSummary.Rows[1][i + 1] = _generator.DcVoltageMax;
                _tableGeneratorSummary.Rows[2][i + 1] = _generator.GeneratorPower;

                _tableGeneratorSummary.Rows[3][i + 1] = _generator.TurbineWindspeedMax;
                _tableGeneratorSummary.Rows[4][i + 1] = _generator.TurbineSpeedTipRatioMax;

                _tableGeneratorSummary.Rows[5][i + 1] = _generator.TurbineWindspeedMin;
                _tableGeneratorSummary.Rows[6][i + 1] = _generator.TurbineSpeedTipRatioMin;

                _tableGeneratorSummary.Rows[7][i + 1] = _generator.TurbineRotorRadius;
                _tableGeneratorSummary.Rows[8][i + 1] = _generator.TurbineMaximumPowerCoefficient;
                _tableGeneratorSummary.Rows[9][i + 1] = _generator.TurbineAirDensity;

                _tableGeneratorSummary.Rows[10][i + 1] = _generator.FrontEndRpmMax;
                _tableGeneratorSummary.Rows[11][i + 1] = _generator.FrontEndRpmMin;
                _tableGeneratorSummary.Rows[12][i + 1] = _generator.FrontEndTorque;

                _tableGeneratorSummary.Rows[13][i + 1] = _generator.PhaseVoltageMin;
                _tableGeneratorSummary.Rows[14][i + 1] = _generator.PhaseVoltageMax;
                _tableGeneratorSummary.Rows[15][i + 1] = _generator.MaxPhaseCurrent;
                _tableGeneratorSummary.Rows[16][i + 1] = _generator.MechamicalGap;
                _tableGeneratorSummary.Rows[17][i + 1] = _generator.GeneratorEfficiency;

                _tableGeneratorSummary.Rows[18][i + 1] = _generator.PhaseCount;
                _tableGeneratorSummary.Rows[19][i + 1] = _generator.CoilsPerPhase;
                _tableGeneratorSummary.Rows[20][i + 1] = _generator.CoilCount;

                _tableGeneratorSummary.Rows[21][i + 1] = _generator.CoilThickness;
                _tableGeneratorSummary.Rows[22][i + 1] = _generator.CoilLegWidth;
                _tableGeneratorSummary.Rows[23][i + 1] = _generator.CoilHeatCoefficient;
                _tableGeneratorSummary.Rows[24][i + 1] = _generator.CoilFillFactor;
                _tableGeneratorSummary.Rows[25][i + 1] = _generator.CoilTurns;
                _tableGeneratorSummary.Rows[26][i + 1] = _generator.CoilWindingCoefficient;
                _tableGeneratorSummary.Rows[27][i + 1] = _generator.CoilWireDiameter;
                _tableGeneratorSummary.Rows[28][i + 1] = _generator.MaxCurrentDensity;
                _tableGeneratorSummary.Rows[29][i + 1] = _generator.CoilWireLength;
                _tableGeneratorSummary.Rows[30][i + 1] = _generator.CoilResistance;
                _tableGeneratorSummary.Rows[31][i + 1] = _generator.CoilInductance;

                _tableGeneratorSummary.Rows[32][i + 1] = _generator.RotorThickness;
                _tableGeneratorSummary.Rows[33][i + 1] = _generator.RotorOuterRadius;
                _tableGeneratorSummary.Rows[34][i + 1] = _generator.RotorInnerRadius;
                _tableGeneratorSummary.Rows[35][i + 1] = _generator.RotorInnerOuterRadiusRatio;
                _tableGeneratorSummary.Rows[36][i + 1] = _generator.MagnetCount;

                _tableGeneratorSummary.Rows[37][i + 1] = _generator.MagnetThickness;
                _tableGeneratorSummary.Rows[38][i + 1] = _generator.MagnetWidth;
                _tableGeneratorSummary.Rows[39][i + 1] = _generator.MagnetLength;

                _tableGeneratorSummary.Rows[40][i + 1] = _generator.MagnetRemanentFluxDensity;
                _tableGeneratorSummary.Rows[41][i + 1] = _generator.MagnetCoerciveFieldStrength;
                _tableGeneratorSummary.Rows[42][i + 1] = _generator.MagnetFluxDensity;
                _tableGeneratorSummary.Rows[43][i + 1] = _generator.MagnetPoleFlux;

                _tableGeneratorSummary.Rows[44][i + 1] = _generator.MagnetDistance;
                _tableGeneratorSummary.Rows[45][i + 1] = _generator.MagnetPoleArcPitch;

                _tableGeneratorSummary.Rows[46][i + 1] = _generator.RectifierDiodeVoltageDrop;

                _tableGeneratorSummary.Rows[47][i + 1] = _generator.PhaseWireLength;
                _tableGeneratorSummary.Rows[48][i + 1] = _generator.PhaseWireDiameter;
                _tableGeneratorSummary.Rows[49][i + 1] = _generator.PhaseWireVoltageDrop;
                _tableGeneratorSummary.Rows[50][i + 1] = _generator.PhaseWireResistance;

                _tableGeneratorSummary.Rows[51][i + 1] = _generator.RectifierWireLength;
                _tableGeneratorSummary.Rows[52][i + 1] = _generator.RectifierWireDiameter;
                _tableGeneratorSummary.Rows[53][i + 1] = _generator.RectifierWireVoltageDrop;
                _tableGeneratorSummary.Rows[54][i + 1] = _generator.RectifierWireResistance;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void UpdateComboBoxes()
        {
            //Battery
            if (cmbEnergyStorage.SelectedIndex == 0)
            {
                _generator.GeneratorEnergyStorageConnection = 0;
                lblInverterVoltageMin.Text = @"Battery voltage (V)";
                numInverterVoltageMax.Visible = false;

                //Wind turbine + Battery
                if (cmbGeneratorFrontEnd.SelectedIndex == 0)
                {
                    _generator.GeneratorFrontEnd = 0;
                    label11.Text = @"Wind turbine front end";

                    numWindSpeedNom.Enabled = true;
                    numTipRatioNom.Enabled = true;
                    numWindSpeedCutIn.Enabled = true;
                    numTipRatioCutIn.Enabled = true;
                    numMaximumPowerCoefficient.Enabled = true;
                    numAirDensity.Enabled = true;
                    lblTurbineRotorRadiusValue.Enabled = true;
                    numRpmMax.Enabled = false;
                    numRpmMin.Enabled = false;

                    //Iterate page
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
                    label11.Text = @"Other front end";

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
                numInverterVoltageMax.Visible = true;

                //Wind turbine + Grid
                if (cmbGeneratorFrontEnd.SelectedIndex == 0)
                {
                    _generator.GeneratorFrontEnd = 0;
                    label11.Text = @"Wind turbine front end";

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
                    label11.Text = @"Other front end";

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

        private void btnIterate_Click(object sender, EventArgs e)
        {
            FillIterationTable();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    chartData1.SaveImage(ms, ChartImageFormat.Bmp);
                    var bm = new Bitmap(ms);
                    bm.Save(chartData1.Name + ".png");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Image save error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (HasNull(_tableGeneratorSummary))
            {
                MessageBox.Show("Iterate data first!");
            }
            else
            {
                try
                {
                    List<double> xValue = new List<double>();
                    List<double> yValue = new List<double>();

                    foreach (var series in chartData1.Series)
                    {
                        series.Points.Clear();
                    }

                    for (int i = 1; i < _tableGeneratorSummary.Columns.Count; i++)
                    {
                        xValue.Add(
                            Convert.ToDouble(_tableGeneratorSummary.Rows[cmbChart1xAxis.SelectedIndex][i].ToString()));
                        yValue.Add(
                            Convert.ToDouble(_tableGeneratorSummary.Rows[cmbChart1yAxis.SelectedIndex][i].ToString()));
                    }

                    if (Math.Abs(xValue.Min() - xValue.Max()) < 0.0000001 ||
                        Math.Abs(yValue.Min() - yValue.Max()) < 0.0000001)
                    {
                        MessageBox.Show("The selected data cannot be charted!");
                    }
                    else
                    {
                        chartData1.Series[0].Points.DataBindXY(xValue, yValue);
                        chartData1.Series[0].ChartType = SeriesChartType.FastLine;
                        chartData1.Series[0].Color = Color.Black;
                        chartData1.ChartAreas[0].AxisX.Maximum = xValue.Max();
                        chartData1.ChartAreas[0].AxisX.Minimum = xValue.Min();
                        chartData1.ChartAreas[0].AxisY.Maximum = yValue.Max();
                        chartData1.ChartAreas[0].AxisY.Minimum = yValue.Min();
                        chartData1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                        chartData1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                        chartData1.ChartAreas[0].AxisX.Title =
                            _tableGeneratorSummary.Rows[cmbChart1xAxis.SelectedIndex][0].ToString();
                        chartData1.ChartAreas[0].AxisY.Title =
                            _tableGeneratorSummary.Rows[cmbChart1yAxis.SelectedIndex][0].ToString();
                        chartData1.Series[0].IsVisibleInLegend = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chart error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (HasNull(_tableGeneratorSummary))
            {
                MessageBox.Show("Iterate data first!");
            }
            else
            {
                try
                {
                    List<double> xValue = new List<double>();
                    List<double> yValue = new List<double>();

                    foreach (var series in chartData2.Series)
                    {
                        series.Points.Clear();
                    }

                    for (int i = 1; i < _tableGeneratorSummary.Columns.Count; i++)
                    {
                        xValue.Add(
                            Convert.ToDouble(_tableGeneratorSummary.Rows[cmbChart2xAxis.SelectedIndex][i].ToString()));
                        yValue.Add(
                            Convert.ToDouble(_tableGeneratorSummary.Rows[cmbChart2yAxis.SelectedIndex][i].ToString()));
                    }

                    if (Math.Abs(xValue.Min() - xValue.Max()) < 0.000000001 ||
                        Math.Abs(yValue.Min() - yValue.Max()) < 0.000000001)
                    {
                        MessageBox.Show("The selected data cannot be charted!");
                    }
                    else
                    {
                        chartData2.Series[0].Points.DataBindXY(xValue, yValue);
                        chartData2.Series[0].ChartType = SeriesChartType.FastLine;
                        chartData2.Series[0].Color = Color.Black;
                        chartData2.ChartAreas[0].AxisX.Maximum = xValue.Max();
                        chartData2.ChartAreas[0].AxisX.Minimum = xValue.Min();
                        chartData2.ChartAreas[0].AxisY.Maximum = yValue.Max();
                        chartData2.ChartAreas[0].AxisY.Minimum = yValue.Min();
                        chartData2.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                        chartData2.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                        chartData2.ChartAreas[0].AxisX.Title =
                            _tableGeneratorSummary.Rows[cmbChart2xAxis.SelectedIndex][0].ToString();
                        chartData2.ChartAreas[0].AxisY.Title =
                            _tableGeneratorSummary.Rows[cmbChart2yAxis.SelectedIndex][0].ToString();
                        chartData2.Series[0].IsVisibleInLegend = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chart error: " + ex.Message);
                }
            }
        }

        private void chart2_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    chartData2.SaveImage(ms, ChartImageFormat.Bmp);
                    var bm = new Bitmap(ms);
                    bm.Save(chartData2.Name + ".png");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Image save error: " + ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool HasNull(DataTable table)
        {
            return
                table.Columns.Cast<DataColumn>().Any(column => table.Rows.OfType<DataRow>().Any(r => r.IsNull(column)));
        }

/*
        private void GenerateImage(Control panel)
        {
            //Save control images for documentation purposes
            var image = new Bitmap(panel.ClientRectangle.Width, panel.ClientRectangle.Height);
            panel.DrawToBitmap(image, panel.ClientRectangle);
            image.Save(panel.Name + ".png");
        }
*/

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            //TODO: Add code to check for changes in the parameters before generating a new model.

            _generatorView.Children.Clear();
            Debug.WriteLine("start");

            //Arguments = @"Python\3dGenerator.py 46 30 9 30 5 1 12 9 4",
            var start = new ProcessStartInfo
            {
                FileName = @"C:\Python27\python.exe",
                Arguments =
                    $@"Python\3dGenerator.py {_generator.MagnetLength} {_generator.MagnetWidth} {_generator.CoilCount} {
                        _generator.CoilLegWidth} {1} {1} {_generator.CoilThickness} {9} {4}",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = false
            };
            Process.Start(start);
            //using (var process = Process.Start(start))
            {
                //if (process == null)
                //{
                //    Debug.WriteLine("null");
                //    return;
                //}
                //else
                //{
                //    Debug.WriteLine("non null");
                //}
                //using (var reader = process.StandardOutput)
                //{
                //    var result = reader.ReadToEnd();
                //    Debug.WriteLine(result);
                //}
                //Debug.WriteLine("Done");
            }
            Debug.WriteLine("Done");
            var lights = new DefaultLights();
            device3D.Content = Display3D("AFPM.obj");
            _generatorView.Children.Add(lights);
            _generatorView.Children.Add(device3D);
            _generatorView.ZoomSensitivity = 3;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
        }

        private Model3D Display3D(string model)
        {
            Model3D device = null;
            try
            {
                //Adding a gesture here
                _generatorView.RotateGesture = new MouseGesture(MouseAction.LeftClick);

                //Import 3D model file
                ModelImporter import = new ModelImporter();

                //Load the 3D model file
                device = import.Load(model);
            }
            catch (Exception e)
            {
                // Handle exception in case can not file 3D model
                MessageBox.Show("Exception Error : " + e.StackTrace);
            }
            return device;
        }
    }
}