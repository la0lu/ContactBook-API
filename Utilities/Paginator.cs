namespace ContactBook.Utilities
{
    public class Paginator
    {
        public int per_page { get; set; }

        public int current_Page { get; set; }


        public Paginator() 
        {
            per_page = 3;
            current_Page = 1;
        }

        public Paginator(int per_page, int current_page)
        {
            this.per_page = per_page > 5 ? 5 : per_page;
            this.current_Page = current_page < 1 ? 1 : current_page;
        }
    }
}
