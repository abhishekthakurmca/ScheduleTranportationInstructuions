import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";
import { GetInstructions, Instructions } from "./@shared/models/instructions";
import { PostInstruction } from "./@shared/models/instruction";
import { schedule } from "./@shared/models/schedule";
import { newSchedule } from "./@shared/models/newInstructionSchedule"

const routes = {
getInstruction: 'https://localhost:7292/api/instructions/',
getSchedule: 'https://localhost:7292/api/schedule'
}

@Injectable({
    providedIn: 'root',
})

export class instructionService {
    constructor(private httpClient: HttpClient) { }

    // this service call instruction get endpoint.
    // this service is created to get list of instructions with product. 
    getAllInstructions(): Observable<GetInstructions[]>{
        return this.httpClient.get<GetInstructions[]>(routes.getInstruction)
    }

    // this service call instruction getbyId endpoint.
    // this service is created to get instruction with its products List. 
    getInstructionById(id: number): Observable<GetInstructions>{
        return this.httpClient.get<GetInstructions>(routes.getInstruction + id);
    }

    // this service call instruction post endpoint.
    // this service is created to save instruction with its products. 
    saveInstruction(instruction: PostInstruction):Observable<PostInstruction>{
        return this.httpClient.post<PostInstruction>(routes.getInstruction, instruction);
    }

    // this service call instruction put endpoint.
    // this service is created to update instruction with its products. 
    updateInstruction(instruction: GetInstructions): Observable<GetInstructions>{
        return this.httpClient.put<GetInstructions>(routes.getInstruction , instruction);
    }

    // this service call instruction delete endpoint.
    // this service is created to delete instruction with its products and its transporter. 
    deleteInstructution(id: number):Observable<Instructions>{
        return this.httpClient.delete<Instructions>(routes.getInstruction + id);    
    }

    // this service call schedule get endpoint.
    // this service is created to get list of instructions with products and product transporter. 
    getAllSchedule():Observable<schedule[]>{
        return this.httpClient.get<schedule[]>(routes.getSchedule)
    }

    // this service call schedule post endpoint.
    // this service is created to assign transporter to product. 
    postSchedule(newSchedule: newSchedule):Observable<newSchedule>{
        return this.httpClient.post<newSchedule>(routes.getSchedule, newSchedule)
    }
}