
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
            if(!bookId) return;

            try{
                // Fetch Book
                const bookResponse = await fetch(`https://library-project-api.fly.dev/GetBookById/${bookId}`);
                const bookData: BookDto = await bookResponse.json();
                setBook(bookData);

                // // Fetch Authors
                if(bookData.authorsIds?.length){
                    const authorResponse =await fetch(`https://library-project-api.fly.dev/GetAllAuthors`);
                    const allAuthors: AuthorDto[] = await authorResponse.json();
                    const bookAuthors = allAuthors.find(a => a.id === bookData.authorsIds);
                    setAuthors(bookAuthors?.name ?? null);
                }

                // Fetch Genre
                if (bookData.genreid) {
                    const genreResponse = await fetch(`https://library-project-api.fly.dev/GetAllGenres`);
                    const allGenres: GenreDto[] = await genreResponse.json();
                    const bookGenre = allGenres.find(g => g.id === bookData.genreid);
                    setGenreName(bookGenre?.name ?? null);
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
        <div className="book-details-container">
            <h1 className="book-title">{book.title}</h1>
            <img  className="book-detail-image" src={book.imageUrl || "placeholder.png"} alt={book.title} />
            <div className="book-info">
                <p><strong>Author: </strong>{authors.length ? authors.map(a => a.name).join(","): "Unknown"}</p>
                <p><strong>Genre: </strong>{genreName || "Unknown"}</p>
                <p><strong>Pages: </strong>{book.pages || "Unknown"}</p>
            </div>
        </div>
    )
}