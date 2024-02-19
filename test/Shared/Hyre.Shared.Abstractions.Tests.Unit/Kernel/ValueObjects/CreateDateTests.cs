// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentAssertions;
using Hyre.Shared.Abstractions.Kernel.Constants;
using Hyre.Shared.Abstractions.Kernel.Exceptions;
using Hyre.Shared.Abstractions.Kernel.ValueObjects;
using NSubstitute;

#endregion

namespace Hyre.Shared.Abstractions.Tests.Unit.Kernel.ValueObjects;

/// <summary>
///   Unit tests for the <see cref="CreateDate" /> class.
/// </summary>
public sealed class CreateDateTests
{
	private readonly TimeProvider _timeProvider = Substitute.For<TimeProvider>();

	[Fact(DisplayName = nameof(Constructor_WhenGivenValidValue_ShouldCreateInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WhenGivenValidValue_ShouldCreateInstance()
	{
		// Arrange
		var value = DateTimeOffset.UtcNow;

		// Act
		var createDate = new CreateDate(value);

		// Assert
		_ = createDate.Value.Should().Be(value);
	}

	[Fact(DisplayName = nameof(Constructor_WhenGivenDefault_ShouldThrowException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WhenGivenDefault_ShouldThrowException()
	{
		// Arrange
		var date = new DateTime(default);
		// Act
		var act = () => new CreateDate(date);

		// Assert
		_ = act.Should()
			.Throw<CreateDateWithEmptyValueException>()
			.WithMessage(EntityErrorMessages.CreateDateWithEmptyValue);
	}

	[Fact(DisplayName = nameof(Constructor_WhenGivenDateInTheFuture_ShouldThrowException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Constructor_WhenGivenDateInTheFuture_ShouldThrowException()
	{
		// Arrange
		var value = DateTimeOffset.UtcNow.AddMinutes(1);

		// Act
		var act = () => new CreateDate(value);

		// Assert
		_ = act.Should()
			.Throw<CreateDateInTheFutureException>()
			.WithMessage(EntityErrorMessages.CreateDateInTheFuture);
	}

	[Fact(DisplayName = nameof(Create_WhenGivenValidValue_ShouldCreateInstance))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Create_WhenGivenValidValue_ShouldCreateInstance()
	{
		// Arrange
		var value = DateTimeOffset.UtcNow;
		_ = _timeProvider.GetUtcNow().Returns(value);

		// Act
		var createDate = CreateDate.Create(_timeProvider);

		// Assert
		_ = createDate.Value.Should().Be(value);
	}

	[Fact(DisplayName = nameof(Create_WhenGivenDefault_ShouldThrowException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Create_WhenGivenDefault_ShouldThrowException()
	{
		// Arrange
		var value = new DateTime(default);
		_ = _timeProvider.GetUtcNow().Returns(value);

		// Act
		var act = () => CreateDate.Create(_timeProvider);

		// Assert
		_ = act.Should()
			.Throw<CreateDateWithEmptyValueException>()
			.WithMessage(EntityErrorMessages.CreateDateWithEmptyValue);
	}

	[Fact(DisplayName = nameof(Create_WhenGivenDateInTheFuture_ShouldThrowException))]
	[Trait(ValueObjectsTraits.Name, ValueObjectsTraits.Value)]
	public void Create_WhenGivenDateInTheFuture_ShouldThrowException()
	{
		// Arrange
		var value = DateTimeOffset.UtcNow.AddMinutes(1);
		_ = _timeProvider.GetUtcNow().Returns(value);

		// Act
		var act = () => CreateDate.Create(_timeProvider);

		// Assert
		_ = act.Should()
			.Throw<CreateDateInTheFutureException>()
			.WithMessage(EntityErrorMessages.CreateDateInTheFuture);
	}
}