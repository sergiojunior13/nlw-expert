using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;

namespace RocketseatAuction.API.UseCases.Auctions.GetCurrent;

public class GetCurrentAuctionUseCase(IAuctionRepository _repository)
{
    public Auction? Execute()
    {
        return _repository.GetCurrent();
    }
}
