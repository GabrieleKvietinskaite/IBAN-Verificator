import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IIban } from 'src/app/models/IIban.interface';
import { IbanService } from 'src/app/services/Iban.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  ibanForm: FormGroup;
  ibansForm: FormGroup;
  submitted = false;
  ibans: IIban[];
  headElements = ['IBAN', 'IS VALID', 'BANK'];

  constructor(private formBuilder: FormBuilder, 
    private ibanService: IbanService) { }

  ngOnInit() {
    this.ibanForm = this.formBuilder.group({
      iban: ['', [ Validators.required, Validators.maxLength(20) ]]
    });

    this.ibansForm = this.formBuilder.group({
      ibans: ['', [ Validators.required ]]
    });
  }

  get fIban(){
    return this.ibanForm.controls;
  }

  get fIbans(){
    return this.ibansForm.controls;
  }

  checkIban(){
    this.submitted = true;

    if(this.ibanForm.invalid) {
      return;
    }

    this.ibanService.getIban(this.fIban.iban.value).subscribe(data =>{
      this.ibans = [];
      this.ibans.push(data);
    });
  }

  checkIbans() {
    this.submitted = true;

    if(this.ibansForm.invalid) {
      return;
    }
    
    let ibans = this.fIbans.ibans.value.split(":").filter(function(iban){
      return iban.length > 0;
    });

    this.ibanService.getIbans(ibans).subscribe((data: IIban[]) =>{
      this.ibans = data;
    });
  }

  clear(){
    this.fIban.iban.setValue('');
    this.fIbans.ibans.setValue('');
    this.submitted = false;
    this.ibans = null;
  }
}
