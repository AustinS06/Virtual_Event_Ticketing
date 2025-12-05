const searchInput = document.querySelector('#searchInput');
if (searchInput) {
    searchInput.addEventListener('input', async (e) => {
        const q = e.target.value;
        const resp = await fetch(`/Events/Search?q=${encodeURIComponent(q)}`);
        const html = await resp.text();
        document.querySelector('#eventsList').innerHTML = html;
    });
}


// Add to cart - simple POST
async function addToCart(eventId, qty = 1) {
    const resp = await fetch('/api/cart/add', {
        method: 'POST', headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ eventId, qty })
    });
    const data = await resp.json();
// Update cart badge
    document.querySelector('#cartCount').textContent = data.count;
}


document.addEventListener('click', (e) => {
    if (e.target.matches('.add-to-cart')) {
        addToCart(e.target.dataset.id);
    }
});
