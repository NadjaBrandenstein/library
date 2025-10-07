
import {useEffect, useState} from "react";
import type {BookDetailsDto} from "./generated-ts-client.ts";
import {useParams} from "react-router";

export default function BookDetails(){
    const {bookId} = useParams<{bookId: string}>()
    const [book, setBook] = useState<BookDetailsDto | null>(null);
    //const [authors, setAuthors] = useState<AuthorDto[]>([]);
    //const [genreName, setGenreName] = useState<string |null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function fecthBookDetails() {
            if(!bookId)
            {
                return;
            }

            try{
                const response = await fetch(`http://localhost:5063/GetBookDetailsById/${bookId}`);
                if (!response.ok) throw new Error("Book not found");

                const data: BookDetailsDto = await response.json();
                setBook(data);

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
                <p><strong>Author: </strong>{book.authors?.length ? book.authors.map(a => a.name).join(","): "Unknown"}</p>
                <p><strong>Genre: </strong>{book.genre?.name || "Unknown"}</p>
                <p><strong>Pages: </strong>{book.pages || "Unknown"}</p>
            </div>
        </div>
    )
}