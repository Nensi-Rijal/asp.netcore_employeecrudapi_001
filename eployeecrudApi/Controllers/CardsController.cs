using eployeecrudApi.Data;
using eployeecrudApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eployeecrudApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardsDbContext cardsDbContext;
        public CardsController(CardsDbContext cardsDbContext)
        {
            this.cardsDbContext = cardsDbContext;
        }
        //Get All Cards
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
           var cards = await cardsDbContext.Cards.ToListAsync();
            return Ok(cards);
        }

        //Get single card
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetSingleCards")]
        public async Task<IActionResult> GetSingleCards([FromRoute] Guid id)
        {
            var card = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (card != null)
            {
                return Ok(card);
            }
            return NotFound("No data");
        }

        //create method
        [HttpPost]
        
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            card.Id = Guid.NewGuid();
           await cardsDbContext.Cards.AddAsync(card);
            await cardsDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingleCards), new { id = card.Id }, card);
    }

        //updating a card
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id,[FromBody] Card card)
        {
            var exisitingcard = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (exisitingcard != null)
            {
                exisitingcard.CardholderName = card.CardholderName;
                exisitingcard.CardNumber = card.CardNumber;
                exisitingcard.ExpiryMonth = card.ExpiryYear;
                exisitingcard.ExpiryYear = card.ExpiryYear;
                exisitingcard.CVC = card.CVC;
                await cardsDbContext.SaveChangesAsync();
                return Ok(exisitingcard);
            }
            return NotFound("card not found");
        }

        //delete a card
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var exisitingcard = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (exisitingcard != null)
            {
                cardsDbContext.Remove(exisitingcard);
                await cardsDbContext.SaveChangesAsync();
                return Ok(exisitingcard);
            }
            return NotFound("card not found");
        }
    }
}
