using Bogus;
using FluentAssertions;
using Moq;
using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Enums;
using RocketseatAuction.API.UseCases.Auctions.GetCurrent;
using Xunit;

namespace UseCases.Test.Auctions.GetCurrent;
public class GetCurrentAuctionUseCaseTest
{
    [Fact]
    public void Success()
    {
        //ARRANGE
        var fakeAuction = new Faker<Auction>()
            .RuleFor(auc => auc.Id, f => f.Random.Number())
            .RuleFor(auc => auc.Name, f => f.Lorem.Word())
            .RuleFor(auc => auc.Starts, f => f.Date.Past())
            .RuleFor(auc => auc.Ends, f => f.Date.Future())
            .RuleFor(auc => auc.Items, (f, auc) => new List<Item> {
                new() {
                Id = f.Random.Number(),
                Name = f.Commerce.ProductName(),
                Brand = f.Commerce.Department(),
                Condition = f.PickRandom<Condition>(),
                AuctionId = auc.Id
            }}).Generate();

        var auctionRepositoryMock = new Mock<IAuctionRepository>();
        auctionRepositoryMock.Setup(i => i.GetCurrent()).Returns(fakeAuction);

        var useCase = new GetCurrentAuctionUseCase(auctionRepositoryMock.Object);

        //ACT
        var auction = useCase.Execute();

        //ASSERT
        auction.Should().NotBeNull();
        auction.Id.Should().Be(fakeAuction.Id);
        auction.Name.Should().Be(fakeAuction.Name);
        auction.Starts.Should().Be(fakeAuction.Starts);
        auction.Ends.Should().Be(fakeAuction.Ends);
        auction.Items.Should().BeSameAs(fakeAuction.Items);
    }
}
