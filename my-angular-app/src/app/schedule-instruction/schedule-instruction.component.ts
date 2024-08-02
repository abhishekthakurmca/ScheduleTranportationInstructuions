import { Component, numberAttribute } from '@angular/core';
import { GetInstructions, Instructions } from '../@shared/models/instructions';
import { transporterList } from '../@shared/Constants/transporterList';
import { instructionService } from '../instruction.service';
import { ToastrService } from 'ngx-toastr';
import { schedule } from '../@shared/models/schedule';
import { newSchedule } from '../@shared/models/newInstructionSchedule';
import { Router } from '@angular/router';

@Component({
  selector: 'app-schedule-instruction',
  templateUrl: './schedule-instruction.component.html',
  styleUrls: ['./schedule-instruction.component.scss']
})
export class ScheduleInstructionComponent {
  instructionList: schedule[] = []
  transporterList: string[] = transporterList.map(x => x.name);
  instructionId: number = 0
  constructor(private service: instructionService,
    private router: Router
  ) {

  }

  ngOnInit() {
    this.getInstruction()
  }

  // method is called to get list of all instructions with products and its transporter
  getInstruction() {
    this.service.getAllSchedule().subscribe({
      next: (response: schedule[]) => {
        this.instructionList = response;
      },
      error: (e) => {
        console.log('Failed to get schedules');
      },
      complete: () => console.log('completed schedules get request')
    })
  }

  // edit button is hidding condition and saving schedule for products
  allowUpdating=(e: any):boolean =>  {
    debugger;

    if (e.row.data.scheduleTransportID == 0 && e.row.data.transporter != null) {
      this.ScheduleInstruction(e.row.data);
    }

    return e.row.data.scheduleTransportID == 0;
  }

  // call add schedule service to save transporter for instruction
  ScheduleInstruction(e: any) {
     this.instructionId = this.instructionList.find(instruction =>
      instruction.products.some(product => product.productId === e.productId))?.id as number;

    var scheduledata = new newSchedule(this.instructionId,e.productId,e.dateScheduled,e.transporter)

    this.service.postSchedule(scheduledata).subscribe({
      next: (response: newSchedule) => {
        console.log(response);
        window.location.reload();
      },
      error: (e) => {
        console.log('Failed to add schedules');
      },
      complete: () => console.log('completed schedules post Request')
    })

  }
}
