public class Program
{
    static void Main(String[] args)
    {
        List<int> list = new List<int>();
        LRUCache cache = new LRUCache(2);
        list.Add(cache.Get(2));
        cache.Put(2,6);
        list.Add(cache.Get(1));
        cache.Put(1, 5);
        cache.Put(1, 2);
        list.Add(cache.Get(1));
        list.Add(cache.Get(2));

        foreach (int i in list)
            Console.Write(i + " ");

    }
}


public class LRUCache
{
    Dictionary<int, DLLNode> map;
    private readonly DLL _dll;
    private readonly int _capacity;
    public LRUCache(int capacity)
    {
        _dll = new DLL();
        map = new Dictionary<int, DLLNode>();
        _capacity = capacity;
    }

    public int Get(int key)
    {
        if (map.ContainsKey(key))
        {
            // run eviction policy to update the recently used node in DLL
            var node = _dll.DetachNode(map[key]);
            _dll.AddToHead(node);

            // return the value of key
            return map[key].value;
        }
        else
        {
            return -1;
        }
    }

    public void Put(int key, int value)
    {

        // if key, already exits get the value, and update it
        if (map.ContainsKey(key))
        {
            Get(key);
            map[key].value = value;
        }
        else
        {

            if (map.Count == _capacity)
            {
                // remove lru from dll
                var node = _dll.DeleteTailNode();

                // remove lru from hashmap
                map.Remove(node.key);

            }

            // else add new value in new Node
            map[key] = new DLLNode(key, value);

            // add in DLL
            _dll.AddToHead(map[key]);
        }

    }
}

public class DLLNode
{
    public int key;
    public int value;
    public DLLNode next;
    public DLLNode prev;

    public DLLNode(int key, int value)
    {
        this.key = key;
        this.value = value;
        next = null;
        prev = null;
    }

    public DLLNode(int key, int value,DLLNode next, DLLNode prev)
    {
        this.key = key;
        this.value = value;
        this.next = next;
        this.prev = prev;
    }
}

public class DLL
{
    public DLLNode head = new DLLNode(-1,-1);
    public DLLNode tail = new DLLNode(-1,-1);

    public DLL()
    {
        head.next = tail;
        tail.prev = head;
    }

    public void AddToHead(DLLNode node)
    {
        node.next = head.next;
        head.next.prev = node;
        head.next = node;
        node.prev = head;
    }

    public DLLNode DetachNode(DLLNode node)
    {
        node.prev.next = node.next;
        node.next.prev = node.prev;
        node.next = null;
        node.prev = null;

        return node;
    }

    public DLLNode DeleteTailNode()
    {
        return DetachNode(tail.prev);
    }

}
