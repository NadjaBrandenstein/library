
import {useEffect, useState} from "react";
import type {AuthorDto, BookDto, GenreDto} from "./generated-ts-client.ts";
import {useParams} from "react-router";

export default function BookDetails(){
    const {bookId} = useParams<{bookId: string}>()
    const [book, setBook] = useState<BookDto | null>(null);
    const [authors, setAuthors] = useState<AuthorDto[]>([]);
    const [genreName, setGenreName] = useState<string |null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function fecthBookDetails() {
            if(!bookId)
            {
                return;
            }

            try{
                // Fetch Book
                const response = await fetch(`https://library-project-api.fly.dev/GetBookById/${bookId}`);
                const bookData: BookDto = await response.json();
                setBook(bookData);

                // Fetch Authors
                try {
                    const authorsResult = await fetch(`https://library-project-api.fly.dev/GetAllAuthors`);
                    const allAuthors: AuthorDto[] = await authorsResult.json();

                    const validAuthorIds = (bookData.authorsIds || []).filter((id): id is string => !!id);

                    const bookAuthors = allAuthors.filter(a => a.id && validAuthorIds.includes(a.id));
                    setAuthors(bookAuthors);
                } catch (err) {
                    console.error("Failed to fetch authors:", err);
                    setAuthors([]);
                }

                // Fetch Genre
                try {
                    if (bookData.genreid) {
                        const genreRes = await fetch(`https://library-project-api.fly.dev/GetAllGenres`);
                        const allGenres: GenreDto[] = await genreRes.json();
                        const bookGenre = allGenres.find(g => g.id === bookData.genreid);
                        setGenreName(bookGenre?.name ?? null);
                    }
                } catch (err) {
                    console.error("Failed to fetch genre:", err);
                    setGenreName(null);
                }

            }catch(err){
                console.error("Failed to fetch book details" + err);
                setBook(null);
            }finally{
                setLoading(false);
            }
        }
        fecthBookDetails();
    },[bookId])

    if(loading){
        return <div>Loading...</div>;
    }

    if(!book){
        return <div>Book not found...</div>
    }

    return (
        <div className="book-details">
            <h1>{book.title}</h1>
            <img src={book.imageUrl || "placeholder.png"} alt={book.title} />
            <p><strong>Author: </strong>{authors.length ? authors.map(a => a.name).join(","): "Unknown"}</p>
            <p><strong>Genre: </strong>{genreName || "Unknown"}</p>
            <p><strong>Pages: </strong>{book.pages || "Unknown"}</p>
        </div>
    )
}