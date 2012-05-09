using FakeItEasy;

using NUnit.Framework;

namespace CsvReader.Core
{
  [TestFixture]
  public class DisplayCoordinatorSpecs
  {
    [Test]
    public void ShouldChangeToNextPageWhen_N_IsPressed()
    {
      var output = A.Fake<IOutput>();
      A
        .CallTo(() => output.Read())
        .ReturnsNextFromSequence('n', 'x');

      var coordinator = new DisplayCoordinator(A.Fake<IFileReader>(), A.Fake<IFormatter>(), output);

      coordinator.Display("file.csv", 42);

      A
        .CallTo(() => output.Read())
        .MustHaveHappened(Repeated.Like(i => i == 2));
    }

    [Test]
    public void ShouldChangeToFirstPageWhen_F_IsPressed()
    {
      var output = A.Fake<IOutput>();
      A
        .CallTo(() => output.Read())
        .ReturnsNextFromSequence('f', 'x');

      var coordinator = new DisplayCoordinator(A.Fake<IFileReader>(), A.Fake<IFormatter>(), output);

      coordinator.Display("file.csv", 42);

      A
        .CallTo(() => output.Read())
        .MustHaveHappened(Repeated.Like(i => i == 2));
    }

    [Test]
    public void ShouldChangeToLastPageWhen_L_IsPressed()
    {
      var output = A.Fake<IOutput>();
      A
        .CallTo(() => output.Read())
        .ReturnsNextFromSequence('l', 'x');

      var coordinator = new DisplayCoordinator(A.Fake<IFileReader>(), A.Fake<IFormatter>(), output);

      coordinator.Display("file.csv", 42);

      A
        .CallTo(() => output.Read())
        .MustHaveHappened(Repeated.Like(i => i == 2));
    }

    [Test]
    public void ShouldChangeToPreviousPageWhen_P_IsPressed()
    {
      var output = A.Fake<IOutput>();
      A
        .CallTo(() => output.Read())
        .ReturnsNextFromSequence('p', 'x');

      var coordinator = new DisplayCoordinator(A.Fake<IFileReader>(), A.Fake<IFormatter>(), output);

      coordinator.Display("file.csv", 42);

      A
        .CallTo(() => output.Read())
        .MustHaveHappened(Repeated.Like(i => i == 2));
    }

    [Test]
    public void ShouldExitWhen_X_IsPressed()
    {
      var output = A.Fake<IOutput>();
      A
        .CallTo(() => output.Read())
        .Returns('x');

      var coordinator = new DisplayCoordinator(A.Fake<IFileReader>(), A.Fake<IFormatter>(), output);

      coordinator.Display("file.csv", 42);
      
      A
        .CallTo(() => output.Read())
        .MustHaveHappened(Repeated.Like(i => i == 1));
    }

    [Test]
    public void ShouldReadModelAndFormatTableAndAskForAction()
    {
      var fileReader = A.Fake<IFileReader>();
      var formatter = A.Fake<IFormatter>();
      var output = A.Fake<IOutput>();
      A
        .CallTo(() => output.Read())
        .Returns('x');
      var coordinator = new DisplayCoordinator(fileReader, formatter, output);
      coordinator.Display("file.csv", 42);

      A
        .CallTo(() => fileReader.Read("file.csv"))
        .MustHaveHappened();

      A
        .CallTo(() => formatter.Format(null))
        .WithAnyArguments()
        .MustHaveHappened();

      A
        .CallTo(() => output.Write(null))
        .WithAnyArguments()
        .MustHaveHappened(Repeated.Like(i => i == 2));

      A
        .CallTo(() => output.Read())
        .MustHaveHappened();
    }
  }
}
