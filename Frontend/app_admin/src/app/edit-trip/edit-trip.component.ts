import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { TripDataService } from '../services/trip-data.service';
import { Trip } from '../models/trip';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-edit-trip',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit-trip.component.html',
  styleUrl: './edit-trip.component.css'
})
export class EditTripComponent implements OnInit {
  public editForm!: FormGroup;
  trip!: Trip;
  submitted = false;
  message: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private tripDataService: TripDataService
  ) {}

  ngOnInit(): void {
    // retrieve cached trip ID
    let tripId = localStorage.getItem("tripId");
    if (!tripId) {
      alert("Something went wrong, couldn't find where I stashed the tripId!");
      this.router.navigate(['']);

      return;
    }


    this.editForm = this.formBuilder.group({
    _id: [tripId],
    code: ['', Validators.required],
    name: ['', Validators.required],
    length: ['', Validators.required],
    start: ['', Validators.required],
    resort: ['', Validators.required],
    perPerson: ['', Validators.required],
    image: ['', Validators.required],
    description: ['', Validators.required]
    })

    this.tripDataService.getTrip(tripId)
      .subscribe({
        next: (value: any) => {
          this.trip = value;
          
          // populate record into the form
          // updated with correct date format for datepicker
          this.editForm.patchValue({
            _id: value._id,
            code: value.code,
            name: value.name,
            length: value.length,
            // formats the date as ISO string w/ yyyy-MM-dd format
            start: formatDate(value.start, 'yyyy-MM-dd', 'en-US'),
            resort: value.resort,
            perPerson: value.perPerson, 
            image: value.image,
            description: value.description
          });
          if (!value) {
            this.message = 'No Trip Retrieved!';
          } else {
            this.message = 'Trip: ' + tripId + ' retrieved';
          }
          console.log(this.message);
        },
        error: (error: any) => {
          console.log('Error: ' + error);
        }
      });
  }

  public onSubmit() {
    this.submitted = true;

    if (this.editForm.valid) {
      this.tripDataService.updateTrip(this.editForm.value)
        .subscribe({
          next: (value: any) => {
            this.router.navigate(['']);
          },
          error: (error: any) => {
            console.log('Error: ' + error);
          }
        });
    }

  }

  // get form short name to access the form fields
  get f() { return this.editForm.controls; }
}
