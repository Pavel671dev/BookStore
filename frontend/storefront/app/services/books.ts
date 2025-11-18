export interface BookRequest {
    title: string;
    description: string;
    price: number;
}

export const getAllBooks = async () => {
    const responce = await fetch("https://localhost:5079/Books");
    
    return responce.json();
}

export const createBook = async (bookRequest: BookRequest) => {
    await fetch("https://localhost:5079/Books/", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(bookRequest),
    });
}

export const updateBook = async (id: string, bookRequest: BookRequest) => {
    await fetch(`https://localhost:5079/Books/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(bookRequest),
    });
}

export const deleteBook = async (id: string) => {
    await fetch(`https://localhost:5079/Books/${id}`, {
        method: "DELETE"
    });
}
