using AutoMapper;
using Shelfish.Api.Dtos.Library;
using Shelfish.Api.Models;

namespace Shelfish.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        /* ---------- Basic entity→DTO maps ---------- */
        CreateMap<Library, LibraryDto>();
        CreateMap<Genre, GenreDto>();
        CreateMap<Bookshelf, BookshelfDto>();
        CreateMap<Book, BookDto>();
        CreateMap<PatronAccount, PatronAccountDto>();
        CreateMap<Review, ReviewDto>();
        CreateMap<Note, NoteDto>();
        CreateMap<ReadingLog, ReadingLogDto>();
        CreateMap<Checkout, CheckoutDto>();

        /* Reverse maps for accepting DTOs */
        CreateMap<LibraryDto, Library>()
            .ReverseMap();
        CreateMap<BookDto, Book>().ReverseMap();
        CreateMap<GenreDto, Genre>().ReverseMap();
        CreateMap<PatronAccountDto, PatronAccount>().ReverseMap();
        CreateMap<ReviewDto, Review>().ReverseMap();
        CreateMap<NoteDto, Note>().ReverseMap();
        CreateMap<ReadingLogDto, ReadingLog>().ReverseMap();
        CreateMap<CheckoutDto, Checkout>().ReverseMap();
    }
}
