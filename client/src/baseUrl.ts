
const isProduction = import.meta.env.prod;

const baseUrl = isProduction
    ? "https://library-project-api.fly.dev"
    : "http://localhost:5063";

// export const bookClient = new BookClient(baseUrl);
// export const authorClient = new AuthorClient(baseUrl);
// export const bookImageClient = new BookImageClient(baseUrl);