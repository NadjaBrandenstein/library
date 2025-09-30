
import {useEffect, useState} from "react";
import type {BookDto} from "./generated-ts-client.ts";
import {useParams} from "react-router";

export default function BookDetails(){
    const {bookId} = useParams<{bookId: string}>()
    const [book, setBook] = useState<BookDto | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function fecthBook() {
            if(!bookId)
            {
                return;
            }

            try{
                const response = await fetch(`https://library-project-api.fly.dev/GetBookById/${bookId}`);
                const data = await response.json();
                setBook(data);
            }catch(err){
                console.error("Failed to fetch book" + err);
                setBook(null);
            }finally{
                setLoading(false);
            }
        }
        fecthBook();
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
            <p><strong>Author: </strong>{book.authorsIds || [].join(",") || "Unknown"}</p>
            <p><strong>Genre: </strong>{book.genreid || "Unknown"}</p>
        </div>
    )
}