using RocketseatAuction.API.Communication.Requests;
using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;

namespace RocketseatAuction.API.UseCases.Offers.CreateOffer;

public class CreateOfferUseCase(ILoggedUser _loggedUser, IOfferRepository _repository)
{
    public int Execute(int itemId, RequestCreateOfferJson req)
    {
        var user = _loggedUser.GetUser();

        var offer = new Offer
        {
            CreatedOn = DateTime.Now,
            ItemId = itemId,
            Price = req.Price,
            UserId = user.Id
        };

        _repository.Add(offer);

        return offer.Id;
    }
}
