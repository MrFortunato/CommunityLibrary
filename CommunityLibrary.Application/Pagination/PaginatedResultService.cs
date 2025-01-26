namespace CommunityLibrary.Application
{
    public class PaginatedResultService<T>
    {
        public IEnumerable<T> Items { get; set; } = [];   
        public int TotalItems { get; set; } // Número total de itens
        public int TotalPages { get; set; } // Número total de páginas
        public int CurrentPage { get; set; } // Número da página atual
        public int PageSize { get; set; } // Quantidade de itens por página
    }
}
