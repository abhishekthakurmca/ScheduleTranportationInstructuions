import { Client } from "../models/Client"
import { Product } from "../models/Product"

// const list of clients.
export const ClientList: Client[] = [
    { ID: 1, Name: "Nikhil", ClientRef:"12569", billingRef: "0000" },
    { ID: 2, Name: "Ajay", ClientRef:"56983", billingRef: "0005" },
    { ID: 3, Name: "Nitin", ClientRef:"85964", billingRef: "0045" },
    { ID: 4, Name: "Aman", ClientRef:"74563", billingRef: "0235" },
    { ID: 5, Name: "Raju", ClientRef:"12589", billingRef: "0235"}]

// const list of products.
export const ProductList: Product[] = [
    {
        product: "Food" ,
        productCode: "0001" ,
        productDescription:"the eatable goods." ,
    },
    {
        product: "Vehicle",
        productCode: "0002",
        productDescription: "the motor vehicle like: car, bike, truck etc."
    },
    {
        product: "Electronic",
        productCode: "0003",
        productDescription: "electronic materials like: phones, juicers, Televisions etc.",
    },
    {
        product: "Breakable",
        productCode: "0004",
        productDescription: "Any goods that can be break easily. like : antiques etc.",
    }]