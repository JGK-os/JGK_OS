namespace OS
{
    public class List<T>
    {
        public      T[]    list;
        private int DefaultSize { get; set; } = 5;
        public int Count { get; set; } = 0;

        public List(int size)
        {
            DefaultSize = size;
            list = new T[DefaultSize];
        }

        public void Add(T obj)
        {
            if (Count == list.Length)
            {
                var tmp = new T[list.Length + 50];

                for (int i = 0 ; i < list.Length ; i++)
                {
                    tmp[i] = list[i];
                }

                list = tmp;
            }

            list[Count++] = obj;
        }

        public void Clear()
        {
            list = new T[DefaultSize];
            Count = 0;
        }
    }
}