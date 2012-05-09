using System;

using FakeItEasy;

using NUnit.Framework;

namespace CsvReader.Core
{
  [TestFixture]
  public class DisplayCoordinatorSpecs
  {
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
    public void ShouldChangeToNextPageWhen_N_IsPressed()
    {
      var output = A.Fake<IOutput>();
      A
        .CallTo(() => output.Read())
        .ReturnsNextFromSequence('n', 'x');

      var fileReader = A.Fake<IFileReader>();
      A
        .CallTo(() => fileReader.Read(null))
        .WithAnyArguments()
        .Returns(new Model()
                   .AddRow(new[] { "one" })
                   .AddRow(new[] { "two" }));

      var coordinator = new DisplayCoordinator(fileReader, A.Fake<IFormatter>(), output);

      coordinator.Display("does-not-exist.csv", 1);

      A
        .CallTo(() => output.WriteLine("Page {0} of {1}", A<object[]>.That.IsSameSequenceAs(new [] {2, 2})))
        .MustHaveHappened();
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
      A
        .CallTo(() => formatter.Format(null))
        .WithAnyArguments()
        .Returns("Die Tabelle");

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
        .CallTo(() => output.Write("Die Tabelle", A<object[]>.Ignored))
        .MustHaveHappened(Repeated.Like(i => i == 1));
      
      A
        .CallTo(() => output.Write(A<string>.That.Contains("N(ext"), A<object[]>.Ignored))
        .MustHaveHappened(Repeated.Like(i => i == 1));

      A
        .CallTo(() => output.Read())
        .MustHaveHappened();
    }

    [Test]
    public void Should_UseNumberedModel()
    {
      var fileReader = A.Fake<IFileReader>();
      A
        .CallTo(() => fileReader.Read(null))
        .WithAnyArguments()
        .Returns(new Model()
                   .SetHeader(new[] { "header" })
                   .AddRow(new[] { "one" })
                   .AddRow(new[] { "two" }));

      var formatter = new TableFormatter();
      var output = A.Fake<IOutput>();
      A
        .CallTo(() => output.Read())
        .Returns('x');

      var coordinator = new DisplayCoordinator(fileReader, formatter, output);
      
      coordinator.Display("file.csv", 42);

      A
        .CallTo(() => output.Write(A<string>.That.StartsWith("No.|header|"), A<object[]>.Ignored))
        .MustHaveHappened();
    }
  }
}
