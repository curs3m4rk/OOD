class LRUCache {
    private final int capacity;
    private final Map<Integer, DLLNode> map;
    private final DLL dll;

    public LRUCache(int capacity) {
        this.capacity = capacity;
        map = new HashMap<>();
        dll = new DLL();
    }

    public int get(int key) {
        if(map.containsKey(key)) {
            // update lru in LL
            DLLNode node = dll.detachNode(map.get(key));
            dll.addToHead(node);

            // return value of key
            return map.get(key).value;
        }
        else {
            return -1;
        }
    }

    public void put(int key, int value) {
        // if key already exits in map, update its value
        if(map.containsKey(key)) {
            get(key);
            map.get(key).value = value;
        }
        else {
            if(map.size() == capacity) {
                // delete LRU from DLL
                DLLNode node = dll.deleteTailNode();

                // remove from hashmap
                map.remove(node.key);
            }

            // add in map and DLL
            map.put(key, new DLLNode(key, value));
            dll.addToHead(map.get(key));
        }
    }
}

class DLLNode {
    int key;
    int value;
    DLLNode next;
    DLLNode prev;

    DLLNode(int key, int value) {
        this.key = key;
        this.value = value;
        next = null;
        prev = null;
    }

    DLLNode(int key, int value, DLLNode next, DLLNode prev) {
        this.key = key;
        this.value = value;
        this.next = next;
        this.prev = prev;
    }
}

class DLL {
    DLLNode head;
    DLLNode tail;

    DLL(){
        head = new DLLNode(-1, -1);
        tail = new DLLNode(-1, -1);
        head.next = tail;
        tail.prev = head;
    }

    public void addToHead(DLLNode node) {
        node.next = head.next;
        head.next.prev = node;
        head.next = node;
        node.prev = head;
    }

    public DLLNode detachNode(DLLNode node) {
        node.prev.next = node.next;
        node.next.prev = node.prev;
        node.next = null;
        node.prev = null;

        return node;
    }

    public DLLNode deleteTailNode() {
        return detachNode(tail.prev);
    }
}