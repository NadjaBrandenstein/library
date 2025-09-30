import {atom} from "jotai";
import type {AuthorDto, BookDto, GenreDto} from "../generated-ts-client.ts";

export const AllBooksAtoms = atom<BookDto[]>([]);
export const AllAuthorsAtoms = atom<AuthorDto[]>([]);
export const AllGenreAtoms = atom<GenreDto[]>([]);