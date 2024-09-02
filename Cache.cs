namespace OOD;

public class Cache
{
    private readonly IStorage _storage;
    private readonly IEvictionPolicy _evictionPolicy;

    public Cache(IStorage storage, IEvictionPolicy evictionPolicy)
    {
        _storage = storage;
        _evictionPolicy = evictionPolicy;
    }

    public string Get(string key)
    {
        string value = _storage.Get(key);
        _evictionPolicy.UpdateLRU(key);
        return value;
    }

    public void Put(string key, string value)
    {
        _storage.Add(key, value);
        _evictionPolicy.AddToMapper(key, value);
        _evictionPolicy.UpdateLRU(key);
    }
}

public interface IStorage
{
    string Get(string key);
    void Add(string key, string value);
}
public class Storage : IStorage
{
    private readonly Dictionary<string, string> _storage;
    private readonly int _capacity;

    public Storage(int capacity)
    {
        _storage = new Dictionary<string, string>();
        _capacity = capacity;
    }

    public string Get(string key)
    {
        return _storage.ContainsKey(key) ? _storage[key] : null;
    }

    public void Add(string key, string value)
    {
        if (_storage.Count == _capacity)
        {
            // Implement eviction logic here (call EvictionPolicy.EvictKey())
            Console.WriteLine("Capacity of Cache is full");
        }
        _storage[key] = value;
    }

    public void remove(string key)
    {
        _storage.Remove(key);
    }
}

public interface IEvictionPolicy
{
    void AddToMapper(string key, string value);
    void UpdateLRU(string key);
    void EvictKey();
}

public class EvictionPolicy : IEvictionPolicy
{
    private readonly DLL _dLL;
    private readonly Dictionary<string, DLLNode> _mapper;

    public EvictionPolicy()
    {
        _dLL = new DLL();
        _mapper = new Dictionary<string, DLLNode>();
    }

    public void AddToMapper(string key, string value)
    {
        DLLNode Node = new DLLNode(value);
        _mapper[key] = Node;
    }
    public void UpdateLRU(string key)
    {
        // Unlink current Node
        _dLL.DetachNode(_mapper[key]);
        // Add to Tail
        _dLL.AddToTail(_mapper[key]);
    }

    public void EvictKey()
    {
        _dLL.EvictKey();
    }
}

public class DLLNode
{
    internal string value;
    internal DLLNode? next;
    internal DLLNode? prev;

    public DLLNode(string value)
    {
        this.value = value;
        next = null;
        prev = null;
    }

    public DLLNode(string value, DLLNode next, DLLNode prev)
    {
        this.value = value;
        this.next = next;
        this.prev = prev;
    }
}

class DLL
{
    internal DLLNode? head = null;
    internal DLLNode? tail = null;

    public void AddToTail(DLLNode node)
    {
        if (head == null)
        {
            head = node;
            tail = node;
        }
        else
        {
            tail.next = node;
            tail = node;
        }
    }

    public void DetachNode(DLLNode node)
    {
        if (head != null)
        {
            return;
        }

        node.next.prev = node.prev;
        node.prev.next = node.next;
        node.next = null;
        node.prev = null;

    }

    public void EvictKey()
    {
        head = head.next;
        head.prev.next = null;
        head.prev = null;
    }
}
