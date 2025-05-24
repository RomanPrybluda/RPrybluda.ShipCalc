using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain.Tests
{
    [TestFixture]
    public class CIIRatingCalculatorWithoutMoqTests
    {
        private CarbonIntensityIndicatorRatingCalculator _calculator;

        [SetUp]
        public void SetUp()
        {
            var capacityCalculator = new CapacityCalculator();
            var refCalculator = new CarbonIntensityIndicatorRefCalculator();
            var requiredCalculator = new CarbonIntensityIndicatorRequiredCalculator();
            var iceClasedShipCapacityCorrFactorCalculator = new IceClasedShipCapacityCorrFactorCalculator();
            var iASuperAndIAIceClassedShipCorrFactorCalculator = new IASuperAndIAIceClassedShipCorrFactorCalculator();

            var attainedCalculator = new CarbonIntensityIndicatorAttainedCalculator(
                iceClasedShipCapacityCorrFactorCalculator,
                iASuperAndIAIceClassedShipCorrFactorCalculator);

            _calculator = new CarbonIntensityIndicatorRatingCalculator(
                capacityCalculator,
                refCalculator,
                requiredCalculator,
                attainedCalculator);
        }

        [Test]
        public void GetCIIRating_BulkCarrier_NIRVANA_ReturnsCorrectRatingAndGrade()
        {
            var ship = new Ship
            {
                ImoNumber = 9519298,
                ShipName = "NIRVANA",
                ShipType = ShipType.BulkCarrier,
                SummerDeadweight = 57015,
                GrossTonnage = 32987,
                BlockCoefficient = 0.7m,
                IceClass = IceClass.NotApplicable
            };

            var refParameters = new CarbonIntensityIndicatorReferenceLineParameter
            {
                ShipType = ShipType.BulkCarrier,
                A = 4740, // Example values; adjust based on actual requirements
                C = 0.622m
            };

            var thresholds = new CarbonIntensityIndicatorRatingThreshold
            {
                ShipType = ShipType.BulkCarrier,
                D1 = 0.86m,
                D2 = 0.94m,
                D3 = 1.06m,
                D4 = 1.18m
            };

            decimal co2EmissionsInTon = 16508m;
            decimal distanceTravelledInNM = 45369m;
            int year = 2024;

            _calculator.CalculateCarbonIntensityIndicatorRatingAsync(
                ship,
                refParameters,
                thresholds,
                co2EmissionsInTon,
                distanceTravelledInNM,
                year);

            decimal expectedAttainedCII = 6.38m;
            decimal expectedRequiredCII = 4.858m;
            decimal expectedCIIref = 5.224m;
            decimal expectedCIINumericRating = 1.314m;
            CarbonIntensityIndicatorRating carbonIntensityIndicatorRating = CarbonIntensityIndicatorRating.E;

            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(_calculator.AttainedCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedAttainedCII).Within(0.02m));

                Assert.That(Math.Round(_calculator.RequiredCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedRequiredCII).Within(0.02m));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorRef, 2),
                    Is.EqualTo(expectedCIIref).Within(0.02m));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorNumericalRating, 2),
                    Is.EqualTo(expectedCIINumericRating).Within(0.02m));

                Assert.That(_calculator.CarbonIntensityIndicatorRating,
                    Is.EqualTo(carbonIntensityIndicatorRating));
            });
        }

        [Test]
        public void GetCIIRating_GeneralCargo_CHELSEA3_ReturnsCorrectRatingAndGrade()
        {
            var ship = new Ship
            {
                ImoNumber = 9361392,
                ShipName = "CHELSEA-3",
                ShipType = ShipType.GeneralCargoShip,
                SummerDeadweight = 5827,
                GrossTonnage = 5170,
                BlockCoefficient = 0.7m,
                IceClass = IceClass.NotApplicable
            };

            var refParameters = new CarbonIntensityIndicatorReferenceLineParameter
            {
                ShipType = ShipType.GeneralCargoShip,
                A = 588,
                C = 0.3885m
            };

            var thresholds = new CarbonIntensityIndicatorRatingThreshold
            {
                ShipType = ShipType.GeneralCargoShip,
                D1 = 0.83m,
                D2 = 0.94m,
                D3 = 1.06m,
                D4 = 1.19m
            };

            decimal co2EmissionsInTon = 1214m;
            decimal distanceTravelledInNM = 13723m;
            int year = 2023;

            _calculator.CalculateCarbonIntensityIndicatorRatingAsync(
                ship,
                refParameters,
                thresholds,
                co2EmissionsInTon,
                distanceTravelledInNM,
                year);

            decimal expectedAttainedCII = 15.18m;
            decimal expectedRequiredCII = 19.241m;
            decimal expectedCIIref = 20.254m;
            decimal expectedCIINumericRating = 0.789m;
            CarbonIntensityIndicatorRating carbonIntensityIndicatorRating = CarbonIntensityIndicatorRating.A;

            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(_calculator.AttainedCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedAttainedCII).Within(0.02m));

                Assert.That(Math.Round(_calculator.RequiredCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedRequiredCII).Within(0.02m));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorRef, 2),
                    Is.EqualTo(expectedCIIref).Within(0.02m));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorNumericalRating, 2),
                    Is.EqualTo(expectedCIINumericRating).Within(0.02m));

                Assert.That(_calculator.CarbonIntensityIndicatorRating,
                    Is.EqualTo(carbonIntensityIndicatorRating));
            });
        }

        [Test]
        public void GetCIIRating_BulkCarrier_IRONDESTINY_ReturnsCorrectRatingAndGrade()
        {
            var ship = new Ship
            {
                ImoNumber = 8202018,
                ShipName = "IRON DESTINY",
                ShipType = ShipType.BulkCarrier,
                SummerDeadweight = 89846,
                GrossTonnage = 54263,
                BlockCoefficient = 0.7m,
                IceClass = IceClass.NotApplicable
            };

            var refParameters = new CarbonIntensityIndicatorReferenceLineParameter
            {
                ShipType = ShipType.BulkCarrier,
                A = 4740, // Example values; adjust based on actual requirements
                C = 0.622m
            };

            var thresholds = new CarbonIntensityIndicatorRatingThreshold
            {
                ShipType = ShipType.BulkCarrier,
                D1 = 0.86m,
                D2 = 0.94m,
                D3 = 1.06m,
                D4 = 1.18m
            };

            decimal co2EmissionsInTon = 3040m;
            decimal distanceTravelledInNM = 643m;
            int year = 2023;

            _calculator.CalculateCarbonIntensityIndicatorRatingAsync(
                ship,
                refParameters,
                thresholds,
                co2EmissionsInTon,
                distanceTravelledInNM,
                year);

            decimal expectedAttainedCII = 52.60m;
            decimal expectedRequiredCII = 3.74m;
            decimal expectedCIIref = 3.937m;
            decimal expectedCIINumericRating = 14.064m;
            CarbonIntensityIndicatorRating carbonIntensityIndicatorRating = CarbonIntensityIndicatorRating.E;

            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(_calculator.AttainedCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedAttainedCII).Within(0.02m));

                Assert.That(Math.Round(_calculator.RequiredCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedRequiredCII).Within(0.02m));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorRef, 2),
                    Is.EqualTo(expectedCIIref).Within(0.02m));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorNumericalRating, 2),
                    Is.EqualTo(expectedCIINumericRating).Within(0.02m));

                Assert.That(_calculator.CarbonIntensityIndicatorRating,
                    Is.EqualTo(carbonIntensityIndicatorRating));
            });
        }

        [Test]
        public void GetCIIRating_Container_HAMBURGTRADER_ReturnsCorrectRatingAndGrade()
        {
            var ship = new Ship
            {
                ImoNumber = 9316098,
                ShipName = "HAMBURG TRADER",
                ShipType = ShipType.ContainerShip,
                SummerDeadweight = 13710,
                GrossTonnage = 9957,
                BlockCoefficient = 0.7m,
                IceClass = IceClass.NotApplicable
            };

            var refParameters = new CarbonIntensityIndicatorReferenceLineParameter
            {
                ShipType = ShipType.ContainerShip,
                A = 1984,
                C = 0.489m
            };

            var thresholds = new CarbonIntensityIndicatorRatingThreshold
            {
                ShipType = ShipType.ContainerShip,
                D1 = 0.83m,
                D2 = 0.94m,
                D3 = 1.07m,
                D4 = 1.19m
            };

            decimal co2EmissionsInTon = 17928m;
            decimal distanceTravelledInNM = 66674m;
            int year = 2023;

            _calculator.CalculateCarbonIntensityIndicatorRatingAsync(
                ship,
                refParameters,
                thresholds,
                co2EmissionsInTon,
                distanceTravelledInNM,
                year);

            decimal expectedAttainedCII = 19.61m;
            decimal expectedRequiredCII = 17.875m;
            decimal expectedCIIref = 18.816m;
            decimal expectedCIINumericRating = 1.097m;
            CarbonIntensityIndicatorRating carbonIntensityIndicatorRating = CarbonIntensityIndicatorRating.D;

            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(_calculator.AttainedCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedAttainedCII).Within(0.02m));

                Assert.That(Math.Round(_calculator.RequiredCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedRequiredCII).Within(0.02m));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorRef, 2),
                    Is.EqualTo(expectedCIIref).Within(0.02m));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorNumericalRating, 2),
                    Is.EqualTo(expectedCIINumericRating).Within(0.02m));

                Assert.That(_calculator.CarbonIntensityIndicatorRating,
                    Is.EqualTo(carbonIntensityIndicatorRating));
            });
        }

        [Test]
        public void GetCIIRating_GeneralCargo_SILVERLION_ReturnsCorrectRatingAndGrade()
        {
            var ship = new Ship
            {
                ImoNumber = 9281487,
                ShipName = "SILVER LION",
                ShipType = ShipType.GeneralCargoShip,
                SummerDeadweight = 6315,
                GrossTonnage = 5197,
                BlockCoefficient = 0.7m,
                IceClass = IceClass.NotApplicable
            };

            var refParameters = new CarbonIntensityIndicatorReferenceLineParameter
            {
                ShipType = ShipType.GeneralCargoShip,
                A = 588,
                C = 0.3885m
            };

            var thresholds = new CarbonIntensityIndicatorRatingThreshold
            {
                ShipType = ShipType.GeneralCargoShip,
                D1 = 0.83m,
                D2 = 0.94m,
                D3 = 1.06m,
                D4 = 1.19m
            };

            decimal co2EmissionsInTon = 1971m;
            decimal distanceTravelledInNM = 18512m;
            int year = 2023;

            _calculator.CalculateCarbonIntensityIndicatorRatingAsync(
                ship,
                refParameters,
                thresholds,
                co2EmissionsInTon,
                distanceTravelledInNM,
                year);

            decimal expectedAttainedCII = 16.86m;
            decimal expectedRequiredCII = 18.649m;
            decimal expectedCIIref = 19.631m;
            decimal expectedCIINumericRating = 0.904m;
            CarbonIntensityIndicatorRating carbonIntensityIndicatorRating = CarbonIntensityIndicatorRating.B;

            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(_calculator.AttainedCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedAttainedCII).Within(0.02m));

                Assert.That(Math.Round(_calculator.RequiredCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedRequiredCII).Within(0.02m));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorRef, 2),
                    Is.EqualTo(expectedCIIref).Within(0.02m));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorNumericalRating, 2),
                    Is.EqualTo(expectedCIINumericRating).Within(0.02m));

                Assert.That(_calculator.CarbonIntensityIndicatorRating,
                    Is.EqualTo(carbonIntensityIndicatorRating));
            });
        }

        [Test]
        public void GetCIIRating_GeneralCargo_KAREWOODPRIDE_ReturnsCorrectRatingAndGrade()
        {
            var ship = new Ship
            {
                ImoNumber = 9363986,
                ShipName = "KAREWOOD PRIDE",
                ShipType = ShipType.GeneralCargoShip,
                SummerDeadweight = 6315,
                GrossTonnage = 5197,
                BlockCoefficient = 0.7m,
                IceClass = IceClass.NotApplicable
            };

            var refParameters = new CarbonIntensityIndicatorReferenceLineParameter
            {
                ShipType = ShipType.GeneralCargoShip,
                A = 588,
                C = 0.3885m
            };

            var thresholds = new CarbonIntensityIndicatorRatingThreshold
            {
                ShipType = ShipType.GeneralCargoShip,
                D1 = 0.83m,
                D2 = 0.94m,
                D3 = 1.06m,
                D4 = 1.19m
            };

            decimal co2EmissionsInTon = 3408m;
            decimal distanceTravelledInNM = 32974m;
            int year = 2023;

            _calculator.CalculateCarbonIntensityIndicatorRatingAsync(
                ship,
                refParameters,
                thresholds,
                co2EmissionsInTon,
                distanceTravelledInNM,
                year);

            decimal expectedAttainedCII = 16.37m;
            decimal expectedRequiredCII = 18.649m;
            decimal expectedCIIref = 19.631m;
            decimal expectedCIINumericRating = 0.878m;
            CarbonIntensityIndicatorRating carbonIntensityIndicatorRating = CarbonIntensityIndicatorRating.B;

            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(_calculator.AttainedCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedAttainedCII).Within(0.02m));

                Assert.That(Math.Round(_calculator.RequiredCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedRequiredCII).Within(0.02m));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorRef, 2),
                    Is.EqualTo(expectedCIIref).Within(0.02m));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorNumericalRating, 2),
                    Is.EqualTo(expectedCIINumericRating).Within(0.02m));

                Assert.That(_calculator.CarbonIntensityIndicatorRating,
                    Is.EqualTo(carbonIntensityIndicatorRating));
            });
        }

        [Test]
        public void GetCIIRating_Container_TITAN_ReturnsCorrectRatingAndGrade()
        {
            var ship = new Ship
            {
                ImoNumber = 9126998,
                ShipName = "TITAN",
                ShipType = ShipType.ContainerShip,
                SummerDeadweight = 14587,
                GrossTonnage = 12029,
                BlockCoefficient = 0.7m,
                IceClass = IceClass.NotApplicable
            };

            var refParameters = new CarbonIntensityIndicatorReferenceLineParameter
            {
                ShipType = ShipType.ContainerShip,
                A = 1984,
                C = 0.489m
            };

            var thresholds = new CarbonIntensityIndicatorRatingThreshold
            {
                ShipType = ShipType.ContainerShip,
                D1 = 0.83m,
                D2 = 0.94m,
                D3 = 1.07m,
                D4 = 1.19m
            };

            decimal co2EmissionsInTon = 8275m;
            decimal distanceTravelledInNM = 21956m;
            int year = 2023;

            _calculator.CalculateCarbonIntensityIndicatorRatingAsync(
                ship,
                refParameters,
                thresholds,
                co2EmissionsInTon,
                distanceTravelledInNM,
                year);

            decimal expectedAttainedCII = 25.84m;
            decimal expectedRequiredCII = 17.341m;
            decimal expectedCIIref = 18.254m;
            decimal expectedCIINumericRating = 1.490m;
            CarbonIntensityIndicatorRating carbonIntensityIndicatorRating = CarbonIntensityIndicatorRating.E;

            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(_calculator.AttainedCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedAttainedCII).Within(0.02m));

                Assert.That(Math.Round(_calculator.RequiredCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedRequiredCII).Within(0.02m));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorRef, 2),
                    Is.EqualTo(expectedCIIref).Within(0.02m));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorNumericalRating, 2),
                    Is.EqualTo(expectedCIINumericRating).Within(0.02m));

                Assert.That(_calculator.CarbonIntensityIndicatorRating,
                    Is.EqualTo(carbonIntensityIndicatorRating));
            });
        }

        [Test]
        public void GetCIIRating_Tanker_LEOPOLD_ReturnsCorrectRatingAndGrade()
        {
            var ship = new Ship
            {
                ImoNumber = 9173032,
                ShipName = "LEOPOLD",
                ShipType = ShipType.Tanker,
                SummerDeadweight = 7076,
                GrossTonnage = 5281,
                BlockCoefficient = 0.7m,
                IceClass = IceClass.NotApplicable
            };

            var refParameters = new CarbonIntensityIndicatorReferenceLineParameter
            {
                ShipType = ShipType.Tanker,
                A = 5247,
                C = 0.610m
            };

            var thresholds = new CarbonIntensityIndicatorRatingThreshold
            {
                ShipType = ShipType.Tanker,
                D1 = 0.82m,
                D2 = 0.93m,
                D3 = 1.08m,
                D4 = 1.28m
            };

            decimal co2EmissionsInTon = 2233m;
            decimal distanceTravelledInNM = 19352m;
            int year = 2024;

            _calculator.CalculateCarbonIntensityIndicatorRatingAsync(
                ship,
                refParameters,
                thresholds,
                co2EmissionsInTon,
                distanceTravelledInNM,
                year);

            decimal expectedAttainedCII = 16.31m;
            decimal expectedRequiredCII = 21.879m;
            decimal expectedCIIref = 23.526m;
            decimal expectedCIINumericRating = 0.745m;
            CarbonIntensityIndicatorRating carbonIntensityIndicatorRating = CarbonIntensityIndicatorRating.A;

            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(_calculator.AttainedCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedAttainedCII).Within(0.02m));

                Assert.That(Math.Round(_calculator.RequiredCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedRequiredCII).Within(0.02m));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorRef, 2),
                    Is.EqualTo(expectedCIIref).Within(0.02m));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorNumericalRating, 2),
                    Is.EqualTo(expectedCIINumericRating).Within(0.02m));

                Assert.That(_calculator.CarbonIntensityIndicatorRating,
                    Is.EqualTo(carbonIntensityIndicatorRating));
            });
        }
    }
}
