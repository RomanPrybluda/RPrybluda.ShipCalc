using Moq;

namespace ShipCalc.Domain.Tests
{
    [TestFixture]
    public class CarbonIntensityIndicatorRatingCalculatorTests
    {
        private CarbonIntensityIndicatorRatingCalculator _calculator;
        private Mock<CarbonIntensityIndicatorRefCalculator> _mockRefCalculator;
        private Mock<CarbonIntensityIndicatorRequiredCalculator> _mockRequiredCalculator;
        private Mock<CarbonIntensityIndicatorAttainedCalculator> _mockAttainedCalculator;
        private Mock<CarbonIntensityIndicatorCalculationValidator> _mockValidator;

        [SetUp]
        public void SetUp()
        {
            _mockRefCalculator = new Mock<CarbonIntensityIndicatorRefCalculator>();
            _mockRequiredCalculator = new Mock<CarbonIntensityIndicatorRequiredCalculator>();
            _mockAttainedCalculator = new Mock<CarbonIntensityIndicatorAttainedCalculator>();
            _mockValidator = new Mock<CarbonIntensityIndicatorCalculationValidator>();
            _calculator = new CarbonIntensityIndicatorRatingCalculator();

            // Setup mock validator to always return true for valid gross tonnage
            _mockValidator.Setup(v => v.ValidateGrossTonnage(It.IsAny<double>())).Returns(true);
        }

        [Test]
        public void CalculateCarbonIntensityIndicatorRating_ContainerShip_ReturnsExpectedRatingAndGrade()
        {
            // Arrange
            var ship = new Ship
            {
                ShipType = ShipType.ContainerShip,
                SummerDeadweight = 12400,
                GrossTonnage = 9616
            };
            _mockRefCalculator.Setup(c => c.CalculateCarbonIntensityIndicatorRef(ship)).Returns(19.763);
            _mockRequiredCalculator.Setup(c => c.CalculateRequiredCII(19.763, 2024)).Returns(18.380);
            _mockAttainedCalculator.Setup(c => c.CalculateAttainedCII(ship, 1271, 11032)).Returns(29.26);

            // Act
            var (rating, letterGrade) = _calculator.CalculateCarbonIntensityIndicatorRating(ship, 1271, 11032, 2024);

            // Assert
            Assert.AreEqual(1.592, rating, 0.001); // Allow small floating-point differences
            Assert.AreEqual(CarbonIntensityIndicatorRating.E, letterGrade);
        }

        [Test]
        public void CalculateCarbonIntensityIndicatorRating_GeneralCargoShip_ReturnsExpectedRatingAndGrade()
        {
            // Arrange
            var ship = new Ship
            {
                ShipType = ShipType.GeneralCargoShip,
                SummerDeadweight = 31642,
                GrossTonnage = 19882
            };
            _mockRefCalculator.Setup(c => c.CalculateCarbonIntensityIndicatorRef(ship)).Returns(8.714);
            _mockRequiredCalculator.Setup(c => c.CalculateRequiredCII(8.714, 2024)).Returns(8.104);
            _mockAttainedCalculator.Setup(c => c.CalculateAttainedCII(ship, 3162, 38088)).Returns(8.21);

            // Act
            var (rating, letterGrade) = _calculator.CalculateCarbonIntensityIndicatorRating(ship, 3162, 38088, 2024);

            // Assert
            Assert.AreEqual(1.013, rating, 0.001);
            Assert.AreEqual(CarbonIntensityIndicatorRating.C, letterGrade);
        }

        [Test]
        public void CalculateCarbonIntensityIndicatorRating_TankerPioneer_ReturnsExpectedRatingAndGrade()
        {
            // Arrange
            var ship = new Ship
            {
                ShipType = ShipType.Tanker,
                SummerDeadweight = 40055,
                GrossTonnage = 22515
            };
            _mockRefCalculator.Setup(c => c.CalculateCarbonIntensityIndicatorRef(ship)).Returns(8.171);
            _mockRequiredCalculator.Setup(c => c.CalculateRequiredCII(8.171, 2024)).Returns(7.599);
            _mockAttainedCalculator.Setup(c => c.CalculateAttainedCII(ship, 2884, 27202)).Returns(8.28);

            // Act
            var (rating, letterGrade) = _calculator.CalculateCarbonIntensityIndicatorRating(ship, 2884, 27202, 2024);

            // Assert
            Assert.AreEqual(1.090, rating, 0.001);
            Assert.AreEqual(CarbonIntensityIndicatorRating.D, letterGrade);
        }

        [Test]
        public void CalculateCarbonIntensityIndicatorRating_TankerOrisSinergy_ReturnsExpectedRatingAndGrade()
        {
            // Arrange
            var ship = new Ship
            {
                ShipType = ShipType.Tanker,
                SummerDeadweight = 13091,
                GrossTonnage = 8542
            };
            _mockRefCalculator.Setup(c => c.CalculateCarbonIntensityIndicatorRef(ship)).Returns(16.164);
            _mockRequiredCalculator.Setup(c => c.CalculateRequiredCII(16.164, 2024)).Returns(15.033);
            _mockAttainedCalculator.Setup(c => c.CalculateAttainedCII(ship, 2190, 26444)).Returns(19.91);

            // Act
            var (rating, letterGrade) = _calculator.CalculateCarbonIntensityIndicatorRating(ship, 2190, 26444, 2024);

            // Assert
            Assert.AreEqual(1.324, rating, 0.001);
            Assert.AreEqual(CarbonIntensityIndicatorRating.E, letterGrade);
        }

        [Test]
        public void CalculateCarbonIntensityIndicatorRating_BulkCarrier_ReturnsExpectedRatingAndGrade()
        {
            // Arrange
            var ship = new Ship
            {
                ShipType = ShipType.BulkCarrier,
                SummerDeadweight = 56562,
                GrossTonnage = 33035
            };
            _mockRefCalculator.Setup(c => c.CalculateCarbonIntensityIndicatorRef(ship)).Returns(5.250);
            _mockRequiredCalculator.Setup(c => c.CalculateRequiredCII(5.250, 2024)).Returns(4.883);
            _mockAttainedCalculator.Setup(c => c.CalculateAttainedCII(ship, 5228, 33024)).Returns(8.77);

            // Act
            var (rating, letterGrade) = _calculator.CalculateCarbonIntensityIndicatorRating(ship, 5228, 33024, 2024);

            // Assert
            Assert.AreEqual(1.796, rating, 0.001);
            Assert.AreEqual(CarbonIntensityIndicatorRating.E, letterGrade);
        }

        [Test]
        public void CalculateCarbonIntensityIndicatorRating_InvalidGrossTonnage_ReturnsZeroAndE()
        {
            // Arrange
            var ship = new Ship
            {
                ShipType = ShipType.Tanker,
                SummerDeadweight = 40055,
                GrossTonnage = 0 // Invalid gross tonnage
            };
            _mockValidator.Setup(v => v.ValidateGrossTonnage(0)).Returns(false);

            // Act
            var (rating, letterGrade) = _calculator.CalculateCarbonIntensityIndicatorRating(ship, 2884, 27202, 2024);

            // Assert
            Assert.AreEqual(0.0, rating);
            Assert.AreEqual(CarbonIntensityIndicatorRating.E, letterGrade);
        }

        [Test]
        public void CalculateCarbonIntensityIndicatorRating_ZeroRequiredCii_ThrowsException()
        {
            // Arrange
            var ship = new Ship
            {
                ShipType = ShipType.Tanker,
                SummerDeadweight = 40055,
                GrossTonnage = 22515
            };
            _mockRefCalculator.Setup(c => c.CalculateCarbonIntensityIndicatorRef(ship)).Returns(8.171);
            _mockRequiredCalculator.Setup(c => c.CalculateRequiredCII(8.171, 2024)).Returns(0.0);
            _mockAttainedCalculator.Setup(c => c.CalculateAttainedCII(ship, 2884, 27202)).Returns(8.28);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculator.CalculateCarbonIntensityIndicatorRating(ship, 2884, 27202, 2024));
        }
    }
}