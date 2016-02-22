using System.Data;
using System.Windows.Forms;
using AxialFluxGeneratorDesigner.Calculations;

namespace AxialFluxGeneratorDesigner.Gui
{
    /// <summary>
    /// </summary>
    public class Iterator
    {
        private const int Iterations = 100;
        private readonly Generator _generator;
        private readonly DataTable _iteratorSummary;
        private readonly FormAfpmDesigner _form;

        public Iterator(FormAfpmDesigner form, Generator gen, DataTable table)
        {

            //dgvGeneratorSummary.RowTemplate.Height = 18;
            //dgvGeneratorSummary.DataSource = _tableGeneratorSummary;
            //dgvGeneratorSummary.Columns[0].Width = 190;

            //var gridViewColumn = dgvGeneratorSummary.Columns["Variable"];
            //if (gridViewColumn != null) gridViewColumn.Frozen = true;

            //foreach (DataRow row in _tableGeneratorSummary.Rows)
            //{
            //    cmbChart1xAxis.Items.Add(row[0]);
            //    cmbChart1yAxis.Items.Add(row[0]);
            //    cmbChart2xAxis.Items.Add(row[0]);
            //    cmbChart2yAxis.Items.Add(row[0]);
            //}

            //cmbChart1xAxis.SelectedIndex = 0;
            //cmbChart1yAxis.SelectedIndex = 0;
            //cmbChart2xAxis.SelectedIndex = 0;
            //cmbChart2yAxis.SelectedIndex = 0;

            _form = form;
            _generator = gen;
            _iteratorSummary = table;

            _iteratorSummary.Columns.Add("Variable", typeof (string));

            _iteratorSummary.Rows.Add("Minimal DC voltage (V)");
            _iteratorSummary.Rows.Add("Maximal DC voltage (V)");
            _iteratorSummary.Rows.Add("Power (W)");

            _iteratorSummary.Rows.Add("Wind speed max (m/s)");
            _iteratorSummary.Rows.Add("Tip speed ratio max");
            _iteratorSummary.Rows.Add("Wind speed min (m/s)");
            _iteratorSummary.Rows.Add("Tip speed ratio min");
            _iteratorSummary.Rows.Add("Rotor radius (m)");
            _iteratorSummary.Rows.Add("Maximum power coefficient");
            _iteratorSummary.Rows.Add("Air density (kg/m3)");

            _iteratorSummary.Rows.Add("RPM max");
            _iteratorSummary.Rows.Add("RPM min");
            _iteratorSummary.Rows.Add("Maximum torque (Nm)");

            _iteratorSummary.Rows.Add("Minimal phase voltage (V)");
            _iteratorSummary.Rows.Add("Maximum phase voltage (V)");
            _iteratorSummary.Rows.Add("Maximum phase current (A)");
            _iteratorSummary.Rows.Add("Mechanical gap (mm)");
            _iteratorSummary.Rows.Add("Generator efficiency (%)");

            _iteratorSummary.Rows.Add("Phase count");
            _iteratorSummary.Rows.Add("Coils per phase");
            _iteratorSummary.Rows.Add("Coils count");
            _iteratorSummary.Rows.Add("Coil thickness (mm)");
            _iteratorSummary.Rows.Add("Coil leg width (mm)");
            _iteratorSummary.Rows.Add("Coil heat coeff. (W/cm2)");
            _iteratorSummary.Rows.Add("Coil Fill factor");
            _iteratorSummary.Rows.Add("Coil Turns");
            _iteratorSummary.Rows.Add("Coil winding factor");
            _iteratorSummary.Rows.Add("Coil wire diameter (mm)");
            _iteratorSummary.Rows.Add("Coil max current density");
            _iteratorSummary.Rows.Add("Coil wire length (m)");
            _iteratorSummary.Rows.Add("Coil resistance (Ohm)");
            _iteratorSummary.Rows.Add("Coil Inductance (mH)");

            _iteratorSummary.Rows.Add("Rotor thickness (mm)");
            _iteratorSummary.Rows.Add("Rotor outer radius (mm)");
            _iteratorSummary.Rows.Add("Rotor inner radius (mm)");
            _iteratorSummary.Rows.Add("Rotor radius ratio");
            _iteratorSummary.Rows.Add("Rotor magnet count");

            _iteratorSummary.Rows.Add("Magnet thickness (mm)");
            _iteratorSummary.Rows.Add("Magnet width (mm)");
            _iteratorSummary.Rows.Add("Magnet length (mm)");
            _iteratorSummary.Rows.Add("Magnet remanence (T)");
            _iteratorSummary.Rows.Add("Magnet field strength(kA/m)");
            _iteratorSummary.Rows.Add("Magnet flux density (T)");
            _iteratorSummary.Rows.Add("Magnet coil (T)");
            _iteratorSummary.Rows.Add("Magnet distance (mm)");
            _iteratorSummary.Rows.Add("Magnet Pole arc / Pole pitch");

            _iteratorSummary.Rows.Add("Diode voltage drop (V)");
            _iteratorSummary.Rows.Add("Phase wire length (m)");
            _iteratorSummary.Rows.Add("Phase wire diameter (mm)");
            _iteratorSummary.Rows.Add("Voltage drop (V)");
            _iteratorSummary.Rows.Add("Wire resistance (Ohm)");
            _iteratorSummary.Rows.Add("Rectifier wire length (m)");
            _iteratorSummary.Rows.Add("Rectifier wire diameter (mm)");
            _iteratorSummary.Rows.Add("Rectifier wire voltage drop (V)");
            _iteratorSummary.Rows.Add("Rectifier wire resistance (Ohm)");


            for (var i = 0; i < Iterations + 1; i++)
            {
                _iteratorSummary.Columns.Add("Iteration " + (i + 1), typeof (double));
            }

            _form.numIterateDcVoltageBatteryMinLower.Value = (decimal) _generator.DcVoltageMin;
            _form.numIterateDcVoltageBatteryMinUpper.Value = (decimal) _generator.DcVoltageMin;
            _form.numIterateDcVoltageBatteryMaxLower.Value = (decimal) _generator.DcVoltageMax;
            _form.numIterateDcVoltageBatteryMaxUpper.Value = (decimal) _generator.DcVoltageMax;
            _form.numShaftPowerLower.Value = (decimal) _generator.GeneratorPower;
            _form.numShaftPowerUpper.Value = (decimal) _generator.GeneratorPower;
            _form.numWindSpeedMaxLower.Value = (decimal) _generator.TurbineWindspeedMax;
            _form.numWindSpeedMaxUpper.Value = (decimal) _generator.TurbineWindspeedMax;
            _form.numSpeedTipRatioMaxLower.Value = (decimal) _generator.TurbineSpeedTipRatioMax;
            _form.numSpeedTipRatioMaxUpper.Value = (decimal) _generator.TurbineSpeedTipRatioMax;
            _form.numWindSpeedMinLower.Value = (decimal) _generator.TurbineWindspeedMin;
            _form.numWindSpeedMinUpper.Value = (decimal) _generator.TurbineWindspeedMin;
            _form.numSpeedTipRatioMinLower.Value = (decimal) _generator.TurbineSpeedTipRatioMin;
            _form.numSpeedTipRatioMinUpper.Value = (decimal) _generator.TurbineSpeedTipRatioMin;
            _form.numMaximumPowerCoefficientLower.Value = (decimal) _generator.TurbineMaximumPowerCoefficient;
            _form.numMaximumPowerCoefficientUpper.Value = (decimal) _generator.TurbineMaximumPowerCoefficient;
            _form.numAirDensityLower.Value = (decimal) _generator.TurbineAirDensity;
            _form.numAirDensityUpper.Value = (decimal) _generator.TurbineAirDensity;
            _form.numOtherRpmMinLower.Value = _generator.FrontEndRpmMin;
            _form.numOtherRpmMinUpper.Value = _generator.FrontEndRpmMin;
            _form.numOtherRpmMaxLower.Value = _generator.FrontEndRpmMax;
            _form.numOtherRpmMaxUpper.Value = _generator.FrontEndRpmMax;
            _form.numMechanicalGapLower.Value = (decimal) _generator.MechamicalGap;
            _form.numMechanicalGapUpper.Value = (decimal) _generator.MechamicalGap;
            _form.numGeneratorEfficiencyLower.Value = (decimal) _generator.GeneratorEfficiency;
            _form.numGeneratorEfficiencyUpper.Value = (decimal) _generator.GeneratorEfficiency;
            _form.numCoilHeatCoefficientLower.Value = (decimal) _generator.CoilHeatCoefficient;
            _form.numCoilHeatCoefficientUpper.Value = (decimal) _generator.CoilHeatCoefficient;
            _form.numCoilFillFactorLower.Value = (decimal) _generator.CoilFillFactor;
            _form.numCoilFillFactorUpper.Value = (decimal) _generator.CoilFillFactor;
            _form.numMagnetThicknessLower.Value = (decimal) _generator.MagnetThickness;
            _form.numMagnetThicknessUpper.Value = (decimal) _generator.MagnetThickness;
            _form.numMagnetLengthLower.Value = (decimal) _generator.MagnetHeight;
            _form.numMagnetLengthUpper.Value = (decimal) _generator.MagnetHeight;
            _form.numMagnetWidthLower.Value = (decimal) _generator.MagnetWidth;
            _form.numMagnetWidthUpper.Value = (decimal) _generator.MagnetWidth;
            _form.numDiodeVoltageDropLower.Value = (decimal) _generator.RectifierDiodeVoltageDrop;
            _form.numDiodeVoltageDropUpper.Value = (decimal) _generator.RectifierDiodeVoltageDrop;
            _form.numPhaseWireLengthLower.Value = (decimal) _generator.PhaseWireLength;
            _form.numPhaseWireLengthUpper.Value = (decimal) _generator.PhaseWireLength;
            _form.numPhaseWireDiameterLower.Value = (decimal) _generator.PhaseWireDiameter;
            _form.numPhaseWireDiameterUpper.Value = (decimal) _generator.PhaseWireDiameter;
            _form.numRectifierWireLengthLower.Value = (decimal) _generator.RectifierWireLength;
            _form.numRectifierWireLengthUpper.Value = (decimal) _generator.RectifierWireLength;
            _form.numRectifierWireDiameterLower.Value = (decimal) _generator.RectifierWireDiameter;
            _form.numRectifierWireDiameterUpper.Value = (decimal) _generator.RectifierWireDiameter;
        }

        public void IterateiteratorSummary()
        {
            for (var i = 0; i < 100 + 1; i++)
            {
                _generator.DcVoltageMin = IterableCalculate(_form.numIterateDcVoltageBatteryMinLower,
                    _form.numIterateDcVoltageBatteryMinUpper, i);
                _generator.DcVoltageMax = IterableCalculate(_form.numIterateDcVoltageBatteryMaxLower,
                    _form.numIterateDcVoltageBatteryMaxUpper, i);
                _generator.GeneratorPower = IterableCalculate(_form.numShaftPowerLower, _form.numShaftPowerUpper, i);

                _generator.TurbineWindspeedMax = IterableCalculate(_form.numWindSpeedMaxLower,
                    _form.numWindSpeedMaxUpper, i);
                _generator.TurbineSpeedTipRatioMax = IterableCalculate(_form.numSpeedTipRatioMaxLower,
                    _form.numSpeedTipRatioMaxUpper, i);
                _generator.TurbineWindspeedMin = IterableCalculate(_form.numWindSpeedMinLower,
                    _form.numWindSpeedMinUpper, i);
                _generator.TurbineSpeedTipRatioMin = IterableCalculate(_form.numSpeedTipRatioMinLower,
                    _form.numSpeedTipRatioMinUpper, i);
                _generator.TurbineMaximumPowerCoefficient = IterableCalculate(_form.numMaximumPowerCoefficientLower,
                    _form.numMaximumPowerCoefficientUpper, i);
                _generator.TurbineAirDensity = IterableCalculate(_form.numAirDensityLower, _form.numAirDensityUpper, i);

                _generator.FrontEndRpmMin =
                    (int) IterableCalculate(_form.numOtherRpmMinLower, _form.numOtherRpmMinUpper, i);
                _generator.FrontEndRpmMax =
                    (int) IterableCalculate(_form.numOtherRpmMaxLower, _form.numOtherRpmMaxUpper, i);

                _generator.MechamicalGap = IterableCalculate(_form.numMechanicalGapLower, _form.numMechanicalGapUpper, i);
                _generator.GeneratorEfficiency = IterableCalculate(_form.numGeneratorEfficiencyLower,
                    _form.numGeneratorEfficiencyUpper, i);
                _generator.CoilHeatCoefficient = IterableCalculate(_form.numCoilHeatCoefficientLower,
                    _form.numCoilHeatCoefficientUpper, i);
                _generator.CoilFillFactor = IterableCalculate(_form.numCoilFillFactorLower, _form.numCoilFillFactorUpper,
                    i);

                _generator.MagnetThickness = IterableCalculate(_form.numMagnetThicknessLower,
                    _form.numMagnetThicknessUpper, i);
                _generator.MagnetHeight = IterableCalculate(_form.numMagnetLengthLower, _form.numMagnetLengthUpper, i);
                _generator.MagnetWidth = IterableCalculate(_form.numMagnetWidthLower, _form.numMagnetWidthUpper, i);

                _generator.RectifierDiodeVoltageDrop = IterableCalculate(_form.numDiodeVoltageDropLower,
                    _form.numDiodeVoltageDropUpper, i);
                _generator.PhaseWireLength = IterableCalculate(_form.numPhaseWireLengthLower,
                    _form.numPhaseWireLengthUpper, i);
                _generator.PhaseWireDiameter = IterableCalculate(_form.numPhaseWireDiameterLower,
                    _form.numPhaseWireDiameterUpper, i);
                _generator.RectifierWireLength = IterableCalculate(_form.numRectifierWireLengthLower,
                    _form.numRectifierWireLengthUpper, i);
                _generator.RectifierWireDiameter = IterableCalculate(_form.numRectifierWireDiameterLower,
                    _form.numRectifierWireDiameterUpper, i);

                _generator.UpdateCalculations(true);

                _iteratorSummary.Rows[0][i + 1] = _generator.DcVoltageMin;
                _iteratorSummary.Rows[1][i + 1] = _generator.DcVoltageMax;
                _iteratorSummary.Rows[2][i + 1] = _generator.GeneratorPower;

                _iteratorSummary.Rows[3][i + 1] = _generator.TurbineWindspeedMax;
                _iteratorSummary.Rows[4][i + 1] = _generator.TurbineSpeedTipRatioMax;

                _iteratorSummary.Rows[5][i + 1] = _generator.TurbineWindspeedMin;
                _iteratorSummary.Rows[6][i + 1] = _generator.TurbineSpeedTipRatioMin;

                _iteratorSummary.Rows[7][i + 1] = _generator.TurbineRotorRadius;
                _iteratorSummary.Rows[8][i + 1] = _generator.TurbineMaximumPowerCoefficient;
                _iteratorSummary.Rows[9][i + 1] = _generator.TurbineAirDensity;

                _iteratorSummary.Rows[10][i + 1] = _generator.FrontEndRpmMax;
                _iteratorSummary.Rows[11][i + 1] = _generator.FrontEndRpmMin;
                _iteratorSummary.Rows[12][i + 1] = _generator.FrontEndTorque;

                _iteratorSummary.Rows[13][i + 1] = _generator.PhaseVoltageMin;
                _iteratorSummary.Rows[14][i + 1] = _generator.PhaseVoltageMax;
                _iteratorSummary.Rows[15][i + 1] = _generator.MaxPhaseCurrent;
                _iteratorSummary.Rows[16][i + 1] = _generator.MechamicalGap;
                _iteratorSummary.Rows[17][i + 1] = _generator.GeneratorEfficiency;

                _iteratorSummary.Rows[18][i + 1] = _generator.PhaseCount;
                _iteratorSummary.Rows[19][i + 1] = _generator.CoilsPerPhase;
                _iteratorSummary.Rows[20][i + 1] = _generator.CoilCount;

                _iteratorSummary.Rows[21][i + 1] = _generator.CoilThickness;
                _iteratorSummary.Rows[22][i + 1] = _generator.CoilLegWidth;
                _iteratorSummary.Rows[23][i + 1] = _generator.CoilHeatCoefficient;
                _iteratorSummary.Rows[24][i + 1] = _generator.CoilFillFactor;
                _iteratorSummary.Rows[25][i + 1] = _generator.CoilTurns;
                _iteratorSummary.Rows[26][i + 1] = _generator.CoilWindingCoefficient;
                _iteratorSummary.Rows[27][i + 1] = _generator.CoilWireDiameter;
                _iteratorSummary.Rows[28][i + 1] = _generator.MaxCurrentDensity;
                _iteratorSummary.Rows[29][i + 1] = _generator.CoilWireLength;
                _iteratorSummary.Rows[30][i + 1] = _generator.CoilResistance;
                _iteratorSummary.Rows[31][i + 1] = _generator.CoilInductance;

                _iteratorSummary.Rows[32][i + 1] = _generator.RotorThickness;
                _iteratorSummary.Rows[33][i + 1] = _generator.RotorOuterRadius;
                _iteratorSummary.Rows[34][i + 1] = _generator.RotorInnerRadius;
                _iteratorSummary.Rows[35][i + 1] = _generator.RotorInnerOuterRadiusRatio;
                _iteratorSummary.Rows[36][i + 1] = _generator.MagnetCount;

                _iteratorSummary.Rows[37][i + 1] = _generator.MagnetThickness;
                _iteratorSummary.Rows[38][i + 1] = _generator.MagnetWidth;
                _iteratorSummary.Rows[39][i + 1] = _generator.MagnetHeight;

                _iteratorSummary.Rows[40][i + 1] = _generator.MagnetRemanentFluxDensity;
                _iteratorSummary.Rows[41][i + 1] = _generator.MagnetCoerciveFieldStrength;
                _iteratorSummary.Rows[42][i + 1] = _generator.MagnetFluxDensity;
                _iteratorSummary.Rows[43][i + 1] = _generator.MagnetPoleFlux;

                _iteratorSummary.Rows[44][i + 1] = _generator.MagnetBetweenDistance;
                _iteratorSummary.Rows[45][i + 1] = _generator.MagnetPoleArcToPolePitchRatio;

                _iteratorSummary.Rows[46][i + 1] = _generator.RectifierDiodeVoltageDrop;

                _iteratorSummary.Rows[47][i + 1] = _generator.PhaseWireLength;
                _iteratorSummary.Rows[48][i + 1] = _generator.PhaseWireDiameter;
                _iteratorSummary.Rows[49][i + 1] = _generator.PhaseWireVoltageDrop;
                _iteratorSummary.Rows[50][i + 1] = _generator.PhaseWireResistance;

                _iteratorSummary.Rows[51][i + 1] = _generator.RectifierWireLength;
                _iteratorSummary.Rows[52][i + 1] = _generator.RectifierWireDiameter;
                _iteratorSummary.Rows[53][i + 1] = _generator.RectifierWireVoltageDrop;
                _iteratorSummary.Rows[54][i + 1] = _generator.RectifierWireResistance;
            }
        }

        private static double IterableCalculate(NumericUpDown lower, NumericUpDown upper, int iteration)
        {
            return (double) lower.Value + ((double) upper.Value - (double) lower.Value)/Iterations*iteration;
        }
    }
}