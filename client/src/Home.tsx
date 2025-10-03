import {useNavigate} from "react-router";
import {AllBooksAtoms} from "./Atoms/Atoms.ts";
import {useAtom} from "jotai";
import {useEffect} from "react";
import type {BookDto} from "./generated-ts-client.ts";


export default function Home(){
    const navigate = useNavigate();
    const [allBooks, setAllBooks] = useAtom(AllBooksAtoms);

    useEffect(() => {
        async function fetchAll() {
            try {
                const bookResponse = await fetch("https://library-project-api.fly.dev/GetAllBooks");
                const data = await bookResponse.json();

                const books: BookDto[] = Array.isArray(data) ? data : data?.books ?? [];
                setAllBooks(books);
            } catch (err) {
                console.error("Failed to fetch books:", err);
                setAllBooks([]);
            }
        }
        fetchAll();
    }, [setAllBooks]);

    if (!allBooks || allBooks.length === 0) {
        return <div>No books available.</div>;
    }

    return (
        <div>
            <div className="book-grid">
                {allBooks.map((book) => (
                    <div
                        key={book.id}
                        className="book-card"
                        onClick={() => navigate("/books/" + book.id)}
                    >
                        <img
                            src={book.imageUrl || "/placeholder.png"}
                            alt={book.title}
                            className="book-image"
                        />
                        <h2 className="book-title">{book.title}</h2>
                    </div>
                ))}
            </div>
        </div>
    );

}