import {AllBooksAtoms} from "./Atoms/Atoms.ts";
import {useNavigate} from "react-router";
import {useState} from "react";
import { useAtom } from "jotai";

export default function NewBook() {
    const [, setAllBooks] = useAtom(AllBooksAtoms)
    const [title, setTitle] = useState('');
    const [author, setAuthor] = useState('');
    const [pages, setPages] = useState<number | "">("");
    const [genre, setGenre] = useState('');
    const [imgurl, setImgurl] = useState("")
    const navigate = useNavigate();

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault();

        try{
            const response = await fetch(`https://library-project-api.fly.dev/CreateBook`, {
                method: "POST",
                headers: {"Content-Type": "application/json"},
                body: JSON.stringify({
                    title: title,
                    name: author,
                    pages: pages,
                    genre: genre,
                    imgurl: imgurl,
                })
            })

            if(!response.ok){
                throw new Error("Failed to create book");
            }

            const newBook = await response.json();

            setAllBooks(prev => [...prev, newBook]);

            navigate("/");
        }
        catch(err){
            console.error(err);
            alert("Failed to create book");
        }
    }

    return(
        <div className="newbook-container">
            <h2>Add new book</h2>
            <form onSubmit={handleSubmit} className="newbook-form">
                <input
                    type="text"
                    placeholder="Title"
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                    required
                    className="newbook-input"
                />
                <input
                    type="text"
                    placeholder="Author"
                    value={author}
                    onChange={(e) => setAuthor(e.target.value)}
                    required
                    className="newbook-input"
                />
                <input
                    type="number"
                    placeholder="Pages"
                    value={pages}
                    onChange={(e) => setPages(Number(e.target.value))}
                    required
                    className="newbook-input"
                />
                <input
                    type="text"
                    placeholder="Genre"
                    value={genre}
                    onChange={(e) => setGenre(e.target.value)}
                    required
                    className="newbook-input"
                />
                <input
                    type="text"
                    placeholder="Image URL"
                    value={imgurl}
                    onChange={(e) => setImgurl(e.target.value)}
                    required
                    className="newbook-input"
                />
                <button type="submit" className="newbook-button">Add Book</button>
            </form>
        </div>
    )

}
