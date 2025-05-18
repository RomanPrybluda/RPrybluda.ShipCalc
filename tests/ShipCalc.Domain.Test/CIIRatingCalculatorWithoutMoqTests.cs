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
                Id = Guid.NewGuid(),
                ImoNumber = 9519298,
                ShipName = "NIRVANA",
                ShipType = ShipType.BulkCarrier,
                SummerDeadweight = 57015,
                GrossTonnage = 32987,
                BlockCoefficient = 0.7,
                IceClass = IceClass.NotApplicable
            };

            double co2EmissionsInTon = 16508;
            double distanceTravelledInNM = 45369;
            int year = 2024;

            _calculator.CalculateCarbonIntensityIndicatorRating(
                ship,
                co2EmissionsInTon,
                distanceTravelledInNM,
                year);

            double expectedAttainedCII = 6.38;
            double expectedRequiredCII = 4.858;
            double expectedCIIref = 5.224;
            double expectedCIINumericRating = 1.314;
            CarbonIntensityIndicatorRating carbonIntensityIndicatorRating = CarbonIntensityIndicatorRating.E;

            double actualAttainedCII = _calculator.AttainedCarbonIntensityIndicator;

            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(_calculator.AttainedCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedAttainedCII).Within(0.02));

                Assert.That(Math.Round(_calculator.RequiredCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedRequiredCII).Within(0.02));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorRef, 2),
                    Is.EqualTo(expectedCIIref).Within(0.02));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorNumericalRating, 2),
                    Is.EqualTo(expectedCIINumericRating).Within(0.02));

                Assert.That(_calculator.CarbonIntensityIndicatorRating,
                    Is.EqualTo(carbonIntensityIndicatorRating));
            });
        }

        [Test]
        public void GetCIIRating_GeneralCargo_CHELSEA3_ReturnsCorrectRatingAndGrade()
        {
            var ship = new Ship
            {
                Id = Guid.NewGuid(),
                ImoNumber = 9361392,
                ShipName = "CHELSEA-3",
                ShipType = ShipType.GeneralCargoShip,
                SummerDeadweight = 5827,
                GrossTonnage = 5170,
                BlockCoefficient = 0.7,
                IceClass = IceClass.NotApplicable
            };

            double co2EmissionsInTon = 1214;
            double distanceTravelledInNM = 13723;
            int year = 2023;

            _calculator.CalculateCarbonIntensityIndicatorRating(
                ship,
                co2EmissionsInTon,
                distanceTravelledInNM,
                year);

            double expectedAttainedCII = 15.18;
            double expectedRequiredCII = 19.241;
            double expectedCIIref = 20.254;
            double expectedCIINumericRating = 0.789;
            CarbonIntensityIndicatorRating carbonIntensityIndicatorRating = CarbonIntensityIndicatorRating.A;

            double actualAttainedCII = _calculator.AttainedCarbonIntensityIndicator;

            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(_calculator.AttainedCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedAttainedCII).Within(0.02));

                Assert.That(Math.Round(_calculator.RequiredCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedRequiredCII).Within(0.02));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorRef, 2),
                    Is.EqualTo(expectedCIIref).Within(0.02));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorNumericalRating, 2),
                    Is.EqualTo(expectedCIINumericRating).Within(0.02));

                Assert.That(_calculator.CarbonIntensityIndicatorRating,
                    Is.EqualTo(carbonIntensityIndicatorRating));
            });
        }

        [Test]
        public void GetCIIRating_BulkCarrier_IRONDESTINY_ReturnsCorrectRatingAndGrade()
        {
            var ship = new Ship
            {
                Id = Guid.NewGuid(),
                ImoNumber = 8202018,
                ShipName = "IRON DESTINY",
                ShipType = ShipType.BulkCarrier,
                SummerDeadweight = 89846,
                GrossTonnage = 54263,
                BlockCoefficient = 0.7,
                IceClass = IceClass.NotApplicable
            };

            double co2EmissionsInTon = 3040;
            double distanceTravelledInNM = 643;
            int year = 2023;

            _calculator.CalculateCarbonIntensityIndicatorRating(
                ship,
                co2EmissionsInTon,
                distanceTravelledInNM,
                year);

            double expectedAttainedCII = 52.60;
            double expectedRequiredCII = 3.74;
            double expectedCIIref = 3.937;
            double expectedCIINumericRating = 14.064;
            CarbonIntensityIndicatorRating carbonIntensityIndicatorRating = CarbonIntensityIndicatorRating.E;

            double actualAttainedCII = _calculator.AttainedCarbonIntensityIndicator;

            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(_calculator.AttainedCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedAttainedCII).Within(0.02));

                Assert.That(Math.Round(_calculator.RequiredCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedRequiredCII).Within(0.02));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorRef, 2),
                    Is.EqualTo(expectedCIIref).Within(0.02));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorNumericalRating, 2),
                    Is.EqualTo(expectedCIINumericRating).Within(0.02));

                Assert.That(_calculator.CarbonIntensityIndicatorRating,
                    Is.EqualTo(carbonIntensityIndicatorRating));
            });
        }

        [Test]
        public void GetCIIRating_Container_HAMBURGTRADER_ReturnsCorrectRatingAndGrade()
        {
            var ship = new Ship
            {
                Id = Guid.NewGuid(),
                ImoNumber = 9316098,
                ShipName = "HAMBURG TRADER",
                ShipType = ShipType.ContainerShip,
                SummerDeadweight = 13710,
                GrossTonnage = 9957,
                BlockCoefficient = 0.7,
                IceClass = IceClass.NotApplicable
            };

            double co2EmissionsInTon = 17928;
            double distanceTravelledInNM = 66674;
            int year = 2023;

            _calculator.CalculateCarbonIntensityIndicatorRating(
                ship,
                co2EmissionsInTon,
                distanceTravelledInNM,
                year);

            double expectedAttainedCII = 19.61;
            double expectedRequiredCII = 17.875;
            double expectedCIIref = 18.816;
            double expectedCIINumericRating = 1.097;
            CarbonIntensityIndicatorRating carbonIntensityIndicatorRating = CarbonIntensityIndicatorRating.D;

            double actualAttainedCII = _calculator.AttainedCarbonIntensityIndicator;

            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(_calculator.AttainedCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedAttainedCII).Within(0.02));

                Assert.That(Math.Round(_calculator.RequiredCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedRequiredCII).Within(0.02));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorRef, 2),
                    Is.EqualTo(expectedCIIref).Within(0.02));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorNumericalRating, 2),
                    Is.EqualTo(expectedCIINumericRating).Within(0.02));

                Assert.That(_calculator.CarbonIntensityIndicatorRating,
                    Is.EqualTo(carbonIntensityIndicatorRating));
            });
        }

        [Test]
        public void GetCIIRating_GeneralCargo_SILVERLION_ReturnsCorrectRatingAndGrade()
        {
            var ship = new Ship
            {
                Id = Guid.NewGuid(),
                ImoNumber = 9281487,
                ShipName = "SILVER LION",
                ShipType = ShipType.GeneralCargoShip,
                SummerDeadweight = 6315,
                GrossTonnage = 5197,
                BlockCoefficient = 0.7,
                IceClass = IceClass.NotApplicable
            };

            double co2EmissionsInTon = 1971;
            double distanceTravelledInNM = 18512;
            int year = 2023;

            _calculator.CalculateCarbonIntensityIndicatorRating(
                ship,
                co2EmissionsInTon,
                distanceTravelledInNM,
                year);

            double expectedAttainedCII = 16.86;
            double expectedRequiredCII = 18.649;
            double expectedCIIref = 19.631;
            double expectedCIINumericRating = 0.904;
            CarbonIntensityIndicatorRating carbonIntensityIndicatorRating = CarbonIntensityIndicatorRating.B;

            double actualAttainedCII = _calculator.AttainedCarbonIntensityIndicator;

            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(_calculator.AttainedCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedAttainedCII).Within(0.02));

                Assert.That(Math.Round(_calculator.RequiredCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedRequiredCII).Within(0.02));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorRef, 2),
                    Is.EqualTo(expectedCIIref).Within(0.02));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorNumericalRating, 2),
                    Is.EqualTo(expectedCIINumericRating).Within(0.02));

                Assert.That(_calculator.CarbonIntensityIndicatorRating,
                    Is.EqualTo(carbonIntensityIndicatorRating));
            });
        }

        [Test]
        public void GetCIIRating_GeneralCargo_KAREWOODPRIDE_ReturnsCorrectRatingAndGrade()
        {
            var ship = new Ship
            {
                Id = Guid.NewGuid(),
                ImoNumber = 9363986,
                ShipName = "KAREWOOD PRIDE",
                ShipType = ShipType.GeneralCargoShip,
                SummerDeadweight = 6315,
                GrossTonnage = 5197,
                BlockCoefficient = 0.7,
                IceClass = IceClass.NotApplicable,
            };

            double co2EmissionsInTon = 3408;
            double distanceTravelledInNM = 32974;
            int year = 2023;

            _calculator.CalculateCarbonIntensityIndicatorRating(
                ship,
                co2EmissionsInTon,
                distanceTravelledInNM,
                year);

            double expectedAttainedCII = 16.37;
            double expectedRequiredCII = 18.649;
            double expectedCIIref = 19.631;
            double expectedCIINumericRating = 0.878;
            CarbonIntensityIndicatorRating carbonIntensityIndicatorRating = CarbonIntensityIndicatorRating.B;

            double actualAttainedCII = _calculator.AttainedCarbonIntensityIndicator;

            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(_calculator.AttainedCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedAttainedCII).Within(0.02));

                Assert.That(Math.Round(_calculator.RequiredCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedRequiredCII).Within(0.02));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorRef, 2),
                    Is.EqualTo(expectedCIIref).Within(0.02));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorNumericalRating, 2),
                    Is.EqualTo(expectedCIINumericRating).Within(0.02));

                Assert.That(_calculator.CarbonIntensityIndicatorRating,
                    Is.EqualTo(carbonIntensityIndicatorRating));
            });
        }

        [Test]
        public void GetCIIRating_Container_TITAN_ReturnsCorrectRatingAndGrade()
        {
            var ship = new Ship
            {
                Id = Guid.NewGuid(),
                ImoNumber = 9126998,
                ShipName = "TITAN",
                ShipType = ShipType.ContainerShip,
                SummerDeadweight = 14587,
                GrossTonnage = 12029,
                BlockCoefficient = 0.7,
                IceClass = IceClass.NotApplicable
            };

            double co2EmissionsInTon = 8275;
            double distanceTravelledInNM = 21956;
            int year = 2023;

            _calculator.CalculateCarbonIntensityIndicatorRating(
                ship,
                co2EmissionsInTon,
                distanceTravelledInNM,
                year);

            double expectedAttainedCII = 25.84;
            double expectedRequiredCII = 17.341;
            double expectedCIIref = 18.254;
            double expectedCIINumericRating = 1.490;
            CarbonIntensityIndicatorRating carbonIntensityIndicatorRating = CarbonIntensityIndicatorRating.E;

            double actualAttainedCII = _calculator.AttainedCarbonIntensityIndicator;

            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(_calculator.AttainedCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedAttainedCII).Within(0.02));

                Assert.That(Math.Round(_calculator.RequiredCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedRequiredCII).Within(0.02));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorRef, 2),
                    Is.EqualTo(expectedCIIref).Within(0.02));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorNumericalRating, 2),
                    Is.EqualTo(expectedCIINumericRating).Within(0.02));

                Assert.That(_calculator.CarbonIntensityIndicatorRating,
                    Is.EqualTo(carbonIntensityIndicatorRating));
            });
        }

        [Test]
        public void GetCIIRating_Tanker_LEOPOLD_ReturnsCorrectRatingAndGrade()
        {
            var ship = new Ship
            {
                Id = Guid.NewGuid(),
                ImoNumber = 9173032,
                ShipName = "LEOPOLD",
                ShipType = ShipType.Tanker,
                SummerDeadweight = 7076,
                GrossTonnage = 5281,
                BlockCoefficient = 0.7,
                IceClass = IceClass.NotApplicable
            };

            double co2EmissionsInTon = 2233;
            double distanceTravelledInNM = 19352;
            int year = 2024;

            _calculator.CalculateCarbonIntensityIndicatorRating(
                ship,
                co2EmissionsInTon,
                distanceTravelledInNM,
                year);

            double expectedAttainedCII = 16.31;
            double expectedRequiredCII = 21.879;
            double expectedCIIref = 23.526;
            double expectedCIINumericRating = 0.745;
            CarbonIntensityIndicatorRating carbonIntensityIndicatorRating = CarbonIntensityIndicatorRating.A;

            double actualAttainedCII = _calculator.AttainedCarbonIntensityIndicator;

            Assert.Multiple(() =>
            {
                Assert.That(Math.Round(_calculator.AttainedCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedAttainedCII).Within(0.02));

                Assert.That(Math.Round(_calculator.RequiredCarbonIntensityIndicator, 2),
                    Is.EqualTo(expectedRequiredCII).Within(0.02));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorRef, 2),
                    Is.EqualTo(expectedCIIref).Within(0.02));

                Assert.That(Math.Round(_calculator.CarbonIntensityIndicatorNumericalRating, 2),
                    Is.EqualTo(expectedCIINumericRating).Within(0.02));

                Assert.That(_calculator.CarbonIntensityIndicatorRating,
                    Is.EqualTo(carbonIntensityIndicatorRating));
            });
        }
    }
}