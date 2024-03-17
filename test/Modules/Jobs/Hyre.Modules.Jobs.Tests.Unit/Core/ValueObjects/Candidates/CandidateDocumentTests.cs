// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Bogus.Extensions.Brazil;
using FluentAssertions;
using Hyre.Modules.Jobs.Core.Constants;
using Hyre.Modules.Jobs.Core.Exceptions.Candidates;
using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;
using Hyre.Modules.Jobs.Tests.Unit.Common;
using Xunit;

#endregion

namespace Hyre.Modules.Jobs.Tests.Unit.Core.ValueObjects.Candidates;

/// <summary>
///   Unit tests for the <see cref="CandidateDocument" /> class.
/// </summary>
public sealed class CandidateDocumentTests : CandidateBaseFixture
{
	[Fact(DisplayName = nameof(Constructor_WithValidParameters_ShouldCreateAnInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithValidParameters_ShouldCreateAnInstance()
	{
		// Arrange
		var generated = Faker.Person.Cpf();

		// Act
		var sut = new CandidateDocument(generated);

		// Assert
		_ = sut.Should().NotBeNull();
		_ = sut.Value.Should().NotBeEmpty();
		_ = sut.Value.Should().Be(generated);
	}

	[Fact(DisplayName = nameof(Constructor_WithInvalidDocument_ShouldThrowAnException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WithInvalidDocument_ShouldThrowAnException()
	{
		// Arrange
		const string invalidDocument = "12345678901";

		// Act
		var act = () => new CandidateDocument(invalidDocument);

		// Assert
		_ = act.Should()
			.ThrowExactly<CandidateDocumentNotValidException>()
			.WithMessage(CandidateErrorMessages.DocumentNotValid);
	}
}